using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Globalization.DateTimeFormatting;
using Windows.System;
using Windows.System.Threading;
using Windows.UI.Core.Preview;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace CBriscolaUWP
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static giocatore g, cpu, primo, secondo, temp;
        private static mazzo m;
        private static carta c, c1, briscola;
        private static BitmapImage cartaCpu = new BitmapImage(new Uri("ms-appx:///Resources/retro carte pc.png"));
        private static Image i, i1;
        private static UInt16 secondi = 5;
        private static TimeSpan delay;
        private static bool avvisaTalloneFinito=true, briscolaDaPunti=false;
        elaboratoreCarteBriscola e;
        public static ResourceMap resourceMap;
        public static ResourceContext resourceContext;
        private Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        private Windows.Storage.ApplicationDataContainer container;

        public MainPage()
        {
            this.InitializeComponent();
            string s;
            SystemNavigationManagerPreview.GetForCurrentView().CloseRequested += Close;

            resourceContext = new Windows.ApplicationModel.Resources.Core.ResourceContext(); // not using ResourceContext.GetForCurrentView
            resourceContext.QualifierValues["Language"] = new DateTimeFormatter("longdate", new[] { CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.ToUpper() }).ResolvedLanguage;
            resourceMap = Windows.ApplicationModel.Resources.Core.ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
            NamedResource res;
            resourceMap.TryGetValue("PuntiDiPrefisso", out res);
            s = res.Resolve(resourceContext).ValueAsString;
            if (s==null) {
                resourceContext.QualifierValues["Language"] = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;

            }
            delay = TimeSpan.FromSeconds(secondi);
            e = new elaboratoreCarteBriscola(briscolaDaPunti);
            m = new mazzo(e);
            carta.inizializza(40, cartaHelperBriscola.getIstanza(e));
            container = localSettings.CreateContainer("CBriscola", Windows.Storage.ApplicationDataCreateDisposition.Always);
            s = localSettings.Containers["CBriscola"].Values["numeUtente"] as string;
            if (s == null)
                s = "numerone";
            g = new giocatore(new giocatoreHelperUtente(), s, 3);
            s = localSettings.Containers["CBriscola"].Values["nomeCpu"] as string;
            if (s == null)
                s = "Cpu";
            cpu = new giocatore(new giocatoreHelperCpu(elaboratoreCarteBriscola.getCartaBriscola()), s, 3);
            primo = g;
            secondo = cpu;
            briscola = carta.getCarta(elaboratoreCarteBriscola.getCartaBriscola());
            s = localSettings.Containers["CBriscola"].Values["secondi"] as string;
            try
            {
                secondi = UInt16.Parse(s);
            }
            catch (Exception ex)
            {
                secondi = 5;
            }
            delay = TimeSpan.FromSeconds(secondi);
            s = localSettings.Containers["CBriscola"].Values["briscolaDaPunti"] as string;
            if (s == null || s == "false")
                briscolaDaPunti = false;
            else
                briscolaDaPunti = true;
            s = localSettings.Containers["CBriscola"].Values["avvisaTalloneFinito"] as string;
            if (s == null || s == "false")
                avvisaTalloneFinito = false;
            else
                avvisaTalloneFinito = true;

            primo = g;
            secondo = cpu;
            briscola = carta.getCarta(elaboratoreCarteBriscola.getCartaBriscola());
            Image[] img = new Image[3];
            for (UInt16 i = 0; i < 3; i++)
            {
                g.addCarta(m);
                cpu.addCarta(m);

            }
            NomeUtente.Text = g.getNome();
            NomeCpu.Text = cpu.getNome();
            Utente0.Source = g.getImmagine(0);
            Utente1.Source = g.getImmagine(1);
            Utente2.Source = g.getImmagine(2);
            Cpu0.Source = cartaCpu;
            Cpu1.Source = cartaCpu;
            Cpu2.Source = cartaCpu;
            PuntiCpu.Text = $"{resourceMap.GetValue("PuntiDiPrefisso", resourceContext).ValueAsString}{cpu.getNome()}{resourceMap.GetValue("PuntiDiSuffisso", resourceContext).ValueAsString}: {cpu.getPunteggio()}";
            PuntiUtente.Text = $"{resourceMap.GetValue("PuntiDiPrefisso", resourceContext).ValueAsString}{g.getNome()}{resourceMap.GetValue("PuntiDiSuffisso", resourceContext).ValueAsString}: {g.getPunteggio()}";
            NelMazzoRimangono.Text = $"{resourceMap.GetValue("NelMazzoRimangono", resourceContext).ValueAsString} {m.getNumeroCarte()} {resourceMap.GetValue("carte", resourceContext).ValueAsString}";
            CartaBriscola.Text = $"{resourceMap.GetValue("SemeBriscola", resourceContext).ValueAsString}: {briscola.getSemeStr()}";
            cbCartaBriscola.Content = resourceMap.GetValue("CartaBriscolaDaPunti", resourceContext).ValueAsString;
            cbAvvisaTallone.Content = resourceMap.GetValue("AvvisaTallone", resourceContext).ValueAsString;
            opNomeUtente.Text = resourceMap.GetValue("NomeUtente", resourceContext).ValueAsString;
            opNomeCpu.Text = resourceMap.GetValue("NomeCpu", resourceContext).ValueAsString;
            Secondi.Text = resourceMap.GetValue("Secondi", resourceContext).ValueAsString;
            InfoApplicazione.Content = resourceMap.GetValue("Applicazione", resourceContext).ValueAsString;
            OpzioniApplicazione.Content = resourceMap.GetValue("Applicazione", resourceContext).ValueAsString;
            OpzioniInformazioni.Content = resourceMap.GetValue("Informazioni", resourceContext).ValueAsString;
            AppInformazioni.Content = resourceMap.GetValue("Informazioni", resourceContext).ValueAsString;
            AppOpzioni.Content = resourceMap.GetValue("Opzioni", resourceContext).ValueAsString;
            btnOk.Content = resourceMap.GetValue("Si", resourceContext).ValueAsString;
            btnCancel.Content = resourceMap.GetValue("No", resourceContext).ValueAsString;
            btnShare.Content = resourceMap.GetValue("Condividi", resourceContext).ValueAsString;
            tbInfo.Text = resourceMap.GetValue("InfoApp", resourceContext).ValueAsString;
            btnInfo.Content = resourceMap.GetValue("MaggioriInfo", resourceContext).ValueAsString;
            Briscola.Source = briscola.getImmagine();
        }
        private Image giocaUtente(Image img)
        {
            UInt16 quale = 0;
            Image img1 = Utente0;
            if (img == Utente1)
            {
                quale = 1;
                img1 = Utente1;
            }
            if (img == Utente2)
            {
                quale = 2;
                img1 = Utente2;
            }
            Giocata0.Visibility = Visibility.Visible;
            Giocata0.Source = img1.Source;
            img1.Visibility = Visibility.Collapsed;
            g.gioca(quale);
            return img1;
        }

        private void OnInfo_Click(object sender, TappedRoutedEventArgs e)
        {
            Applicazione.Visibility = Visibility.Collapsed;
            GOpzioni.Visibility = Visibility.Collapsed;
            Info.Visibility = Visibility.Visible;
        }

        private void OnApp_Click(object sender, TappedRoutedEventArgs e)
        {
            GOpzioni.Visibility = Visibility.Collapsed;
            Info.Visibility = Visibility.Collapsed;
            Applicazione.Visibility = Visibility.Visible;
        }
        private void OnOpzioni_Click(object sender, TappedRoutedEventArgs e)
        {
            txtNomeUtente.Text = g.getNome();
            txtCpu.Text = cpu.getNome();
            txtSecondi.Text = secondi.ToString();
            cbCartaBriscola.IsChecked = briscolaDaPunti;
            cbAvvisaTallone.IsChecked = avvisaTalloneFinito;
            Info.Visibility = Visibility.Collapsed;
            Applicazione.Visibility = Visibility.Collapsed;
            GOpzioni.Visibility = Visibility.Visible;
        }

        private void SalvaOpzioni()
        {
            localSettings.Containers["CBriscola"].Values["numeUtente"] = txtNomeUtente.Text;
            localSettings.Containers["CBriscola"].Values["nomeCpu"] = txtCpu.Text;
            localSettings.Containers["CBriscola"].Values["secondi"] = $"{secondi}";
            if (cbCartaBriscola.IsChecked == null || cbCartaBriscola.IsChecked == false)
            {
                briscolaDaPunti = false;
                localSettings.Containers["CBriscola"].Values["briscolaDaPunti"] = "false";
            }
            else
            {
                localSettings.Containers["CBriscola"].Values["briscolaDaPunti"] = "true";
                briscolaDaPunti = true;
            }
            if (cbAvvisaTallone.IsChecked == null || cbAvvisaTallone.IsChecked == false)
            {
                avvisaTalloneFinito = false;
                localSettings.Containers["CBriscola"].Values["avvisaTalloneFinito"] = "false";
            }
            else
            {
                avvisaTalloneFinito = true;
                localSettings.Containers["CBriscola"].Values["avvisaTalloneFinito"] = "true";
            }

        }

        private void OnOkFp_Click(object sender, TappedRoutedEventArgs evt)
        {
            bool cartaBriscola = true;
            FinePartita.Visibility = Visibility.Collapsed;
            if (cbCartaBriscola.IsChecked == null || cbCartaBriscola.IsChecked == false)
                cartaBriscola = false;
            e = new elaboratoreCarteBriscola(cartaBriscola) ;
            m = new mazzo(e);
            briscola = carta.getCarta(elaboratoreCarteBriscola.getCartaBriscola());
            g = new giocatore(new giocatoreHelperUtente(), g.getNome(), 3);
            cpu = new giocatore(new giocatoreHelperCpu(elaboratoreCarteBriscola.getCartaBriscola()), cpu.getNome(), 3);
            for (UInt16 i = 0; i < 3; i++)
            {
                g.addCarta(m);
                cpu.addCarta(m);

            }
            Utente0.Source = g.getImmagine(0);
            Utente0.Visibility = Visibility.Visible;
            Utente1.Source = g.getImmagine(1);
            Utente1.Visibility = Visibility.Visible;
            Utente2.Source = g.getImmagine(2);
            Utente2.Visibility = Visibility.Visible;
            Cpu0.Source = cartaCpu;
            Cpu0.Visibility = Visibility.Visible;
            Cpu1.Source = cartaCpu;
            Cpu1.Visibility = Visibility.Visible;
            Cpu2.Source = cartaCpu;
            Cpu2.Visibility = Visibility.Visible;
            Giocata0.Visibility = Visibility.Collapsed;
            Giocata1.Visibility = Visibility.Collapsed;
            PuntiCpu.Text = $"{resourceMap.GetValue("PuntiDiPrefisso", resourceContext).ValueAsString}{cpu.getNome()}{resourceMap.GetValue("PuntiDiSuffisso", resourceContext).ValueAsString}: {cpu.getPunteggio()}";
            PuntiUtente.Text = $"{resourceMap.GetValue("PuntiDiPrefisso", resourceContext).ValueAsString}{g.getNome()}{resourceMap.GetValue("PuntiDiSuffisso", resourceContext).ValueAsString}: {g.getPunteggio()}";
            NelMazzoRimangono.Text = $"{resourceMap.GetValue("NelMazzoRimangono", resourceContext).ValueAsString} {m.getNumeroCarte()} {resourceMap.GetValue("carte", resourceContext).ValueAsString}";
            NelMazzoRimangono.Visibility = Visibility.Visible;
            CartaBriscola.Text = $"{resourceMap.GetValue("SemeBriscola", resourceContext).ValueAsString}: {briscola.getSemeStr()}";
            CartaBriscola.Visibility = Visibility.Visible;
            Briscola.Source = briscola.getImmagine();
            Briscola.Visibility = Visibility.Visible;
            primo = g;
            secondo = cpu;
            Briscola.Source = briscola.getImmagine();
            Applicazione.Visibility = Visibility.Visible;
        }
        private void OnCancelFp_Click(object sender, TappedRoutedEventArgs e)
        {
            Application.Current.Exit();
        }
        private Image giocaCpu()
        {
            UInt16 quale = 0;
            Image img1 = Cpu0;
            if (primo == cpu)
                cpu.gioca(0);
            else
                cpu.gioca(0, g);
            quale = cpu.getICartaGiocata();
            if (quale == 1)
                img1 = Cpu1;
            if (quale == 2)
                img1 = Cpu2;
            Giocata1.Visibility = Visibility.Visible;
            Giocata1.Source = cpu.getCartaGiocata().getImmagine();
            img1.Visibility = Visibility.Collapsed;
            return img1;
        }
        private static bool aggiungiCarte()
        {
            try
            {
                primo.addCarta(m);
                secondo.addCarta(m);
            }
            catch (IndexOutOfRangeException e)
            {
                return false;
            }
            return true;
        }

        private void Image_Tapped(object Sender, TappedRoutedEventArgs arg)
        {
            Image img = (Image)Sender;
            i = giocaUtente(img);
            if (secondo == cpu)
                i1 = giocaCpu();
            ThreadPoolTimer t = ThreadPoolTimer.CreateTimer((source) =>
            {

                IAsyncAction asyncAction = Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High, () =>
                {

                    c = primo.getCartaGiocata();
                    c1 = secondo.getCartaGiocata();
                    if ((c.CompareTo(c1) > 0 && c.stessoSeme(c1)) || (c1.stessoSeme(briscola) && !c.stessoSeme(briscola)))
                    {
                        temp = secondo;
                        secondo = primo;
                        primo = temp;
                    }

                    primo.aggiornaPunteggio(secondo);
                    PuntiCpu.Text = $"{resourceMap.GetValue("PuntiDiPrefisso", resourceContext).ValueAsString}{cpu.getNome()}{resourceMap.GetValue("PuntiDiSuffisso", resourceContext).ValueAsString}: {cpu.getPunteggio()}";
                    PuntiUtente.Text = $"{resourceMap.GetValue("PuntiDiPrefisso", resourceContext).ValueAsString}{g.getNome()}{resourceMap.GetValue("PuntiDiSuffisso", resourceContext).ValueAsString}: {g.getPunteggio()}";
                    if (aggiungiCarte())
                    {
                        NelMazzoRimangono.Text = $"{resourceMap.GetValue("NelMazzoRimangono", resourceContext).ValueAsString} {m.getNumeroCarte()} {resourceMap.GetValue("carte", resourceContext).ValueAsString}";
                        CartaBriscola.Text = $"{resourceMap.GetValue("SemeBriscola", resourceContext).ValueAsString}: {briscola.getSemeStr()}";
                        if (m.getNumeroCarte()==2 && avvisaTalloneFinito)
                            new ToastContentBuilder().AddArgument(resourceMap.GetValue("TalloneFinito", resourceContext).ValueAsString).AddText(resourceMap.GetValue("IlTalloneEFinito", resourceContext).ValueAsString).AddAudio(new Uri("ms-winsoundevent:Notification.Reminder")).Show();
                        if (Briscola.Visibility == Visibility.Visible && m.getNumeroCarte() == 0)
                        {
                            NelMazzoRimangono.Visibility = Visibility.Collapsed;
                            Briscola.Visibility = Visibility.Collapsed;
                        }
                        Utente0.Source = g.getImmagine(0);
                        if (cpu.getNumeroCarte() > 1)
                            Utente1.Source = g.getImmagine(1);
                        if (cpu.getNumeroCarte() > 2)
                            Utente2.Source = g.getImmagine(2);
                        i.Visibility = Visibility.Visible;
                        i1.Visibility = Visibility.Visible;
                        Giocata0.Visibility = Visibility.Collapsed;
                        Giocata1.Visibility = Visibility.Collapsed;
                        if (cpu.getNumeroCarte() == 2)
                        {
                            Utente2.Visibility = Visibility.Collapsed;
                            Cpu2.Visibility = Visibility.Collapsed;
                        }
                        if (cpu.getNumeroCarte() == 1)
                        {
                            Utente1.Visibility = Visibility.Collapsed;
                            Cpu1.Visibility = Visibility.Collapsed;
                        }
                        if (primo == cpu)
                        {
                            i1 = giocaCpu();
                            if (cpu.getCartaGiocata().stessoSeme(briscola))
                                new ToastContentBuilder().AddArgument(resourceMap.GetValue("GiocataBriscola", resourceContext).ValueAsString).AddText($"{resourceMap.GetValue("LaCpuHaGiocatoIl", resourceContext).ValueAsString} {cpu.getCartaGiocata().getValore() + 1} {resourceMap.GetValue("diBriscola", resourceContext).ValueAsString}").AddAudio(new Uri("ms-winsoundevent:Notification.Reminder")).Show();
                            else if (cpu.getCartaGiocata().getPunteggio() > 0)
                                new ToastContentBuilder().AddArgument(resourceMap.GetValue("GiocataCartaDiValore", resourceContext).ValueAsString).AddText($"{resourceMap.GetValue("LaCpuHaGiocatoIl", resourceContext).ValueAsString} {cpu.getCartaGiocata().getValore() + 1} {resourceMap.GetValue("di", resourceContext).ValueAsString} {cpu.getCartaGiocata().getSemeStr()}").AddAudio(new Uri("ms-winsoundevent:Notification.Reminder")).Show();
                        }

                    }
                    else
                    {
                        string s;
                        if (g.getPunteggio() == cpu.getPunteggio())
                            s = resourceMap.GetValue("PartitaPatta", resourceContext).ValueAsString;
                        else
                        {
                            if (g.getPunteggio() > cpu.getPunteggio())
                                s = resourceMap.GetValue("HaiVinto", resourceContext).ValueAsString;
                            else
                                s = resourceMap.GetValue("HaiPerso", resourceContext).ValueAsString;
                            s = $"{s} {resourceMap.GetValue("per", resourceContext).ValueAsString} {Math.Abs(g.getPunteggio() - cpu.getPunteggio())}  {resourceMap.GetValue("punti", resourceContext).ValueAsString}";
                        }
                        fpRisultrato.Text = $"{resourceMap.GetValue("PartitaFinita", resourceContext).ValueAsString}. {s} {resourceMap.GetValue("SecondaPartita", resourceContext).ValueAsString}";
                        Applicazione.Visibility = Visibility.Collapsed;
                        FinePartita.Visibility = Visibility.Visible;
                    }
                });
            }, delay);
        }
        public void OnOk_Click(Object source, RoutedEventArgs evt)
        {
            g.setNome(txtNomeUtente.Text);
            cpu.setNome(txtCpu.Text);
            if (cbCartaBriscola.IsChecked==null || cbCartaBriscola.IsChecked==false)
                briscolaDaPunti = false;
            else
                briscolaDaPunti = true;
            if (cbAvvisaTallone.IsChecked==null || cbAvvisaTallone.IsChecked==false)
                avvisaTalloneFinito = false;
            else
                avvisaTalloneFinito = true;
            try
            {
                secondi = UInt16.Parse(txtSecondi.Text);
            } catch (FormatException ex)
            {
                txtSecondi.Text = resourceMap.GetValue("ValoreNonValido", resourceContext).ValueAsString; ;
                return;
            }
            delay = TimeSpan.FromSeconds(secondi);
            NomeUtente.Text = g.getNome();
            NomeCpu.Text = cpu.getNome();
            SalvaOpzioni();
            GOpzioni.Visibility = Visibility.Collapsed;
            Applicazione.Visibility = Visibility.Visible;
        }

        private async void OnFPShare_Click(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri($"https://twitter.com/intent/tweet?text=Con%20la%20CBriscola%20la%20partita%20{g.getNome()}%20contro%20{cpu.getNome()}%20%C3%A8%20finita%20{g.getPunteggio()}%20a%20{cpu.getPunteggio()}&url=https%3A%2F%2Fgithub.com%2Fnumerunix%2Fcbriscolauwp.new"));
        }


        private async void OnSito_Click(object sender, TappedRoutedEventArgs e)
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/numerunix/cbriscolauwp.new"));
        }

        public void Close(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            container.Dispose();
        }
    }
}
