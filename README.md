# CBriscolaUWP per il windows store
La cbriscola grafica in UWP, il falso vero Project Carmela

# Come ottenere
[![microsoft](https://get.microsoft.com/images/en-us%20dark.svg)](https://www.microsoft.com/store/apps/9NX1JKTQK12C)

# Bug noti

Su Xbox, per cambiare linguaggio biosgna disinstallare e reinstallare l'applicativo.

# The old fashion compiled way

Installa visual studio 2022, scarica il progetto usando git selezionando download from existing git repository e quindi compilalo.

# Come provarlo su xbox.
Andate su https://developer.microsoft.com/it-it/microsoft-store/register/ e registrate il vostro account hotmail (possibilmente non quello principale, ma createne uno  nuovo) come developer.
Pagate i 17 euro, non si capisce se una tantum oppure annui, ed aspettate l'attivazione, dopodiché aprite sulla vostra SECONDA (non di gioco) xbox one o series x l'app store e scaricate l'app dev kit (quella con le icone della series s/x, non quella con l'icona della one), apritela e seguite le istruzioni a video, facendo attenzione a quando vi dice "aprite windows 10 aprite, aprite aka.ms/quello_che_è e registrate la xbox), ad aprire da pc nel vostro account developer microsoft la sezione "xbox one development consoles", premere il tasto "+" in alto a destra, selezionare "enter activation code" ed indicare il codice mostrato a video; quando l'impostazione è finita la console viene riavviata e si entra nel developer mode.

Per prima cosa registrate il vostro account di developer nell'apposita sezione in alto a destra, dopo cliccate su "show visual studio pin" e salvatevelo, ora pasate al pc, aprite il progetto, cliccate su dispositivo remoto invece che su computer locale quando dovete compilare, indicate l'ip della xbox, aspettate che vi chiede il pin, inseritelo e compilate.

A breve nella sezione bassa centrale comparirà la cbriscola, basta avviarla per poter giocare.

# Donazione

http://numerone.altervista.org/donazioni.php

# Bibliografia

https://stackoverflow.com/questions/39166595/uwp-get-current-cultureinfo

https://code.4noobz.net/uwp-how-to-get-the-close-app-event/

https://learn.microsoft.com/en-us/uwp/api/windows.storage.applicationdatacontainer?view=winrt-22621

https://social.msdn.microsoft.com/Forums/vstudio/en-US/2d8a7dab-1bad-4405-b70d-768e4cb2af96/uwp-get-os-version-in-an-uwp-app?forum=wpdevelop
