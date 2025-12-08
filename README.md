Il progetto è dedicato a Carmela, la badante di mia nonna paterna che prima di andarsene voleva presentarmi la mia attuale fidanzata.

:it: Made in Italy. Il primo gioco indipendente (non indie) free software per la xbox.

## ATTENZIONE

come farsi spiegare il readme da gemini: https://g.co/gemini/share/e0d786fc8f22 , basta loggarsi con l'account Google.

Il progetto così come è funziona, solo che venite invitati a ricompilarlo su pc impostando il debug sulla prima linea del main, scoprendo come si chiama la variabile di sistema che contiene la mainwindow ed a impostarla a NULL.

Il progetto viene dichiarato legacy, per il momento.
Su windows c'è la briscola in maui o in material, mentre su xbox al momento non c'è soluzione, ma ci stanno lavorando.

## Descrizione

Questo gioco dimostra che la teoria dei giochi è vera: l'algorimo brevettato funziona su tutti i giochi di carte senza piatto.

![Napoli-Logo](https://github.com/user-attachments/assets/b0218b39-2d99-4ce0-8c43-ac42b512b710)

![made in parco grifeo](https://github.com/user-attachments/assets/483574bc-e34b-4560-9a95-b5267290a1a8)



## CBriscolaUWP per il windows store
La cbriscola grafica in UWP, il falso vero Project Carmela.
Quello che avete davanti non è il gioco della briscola come si intende oggi, perché oggi tutti i simulatori di briscola dicono "hai preso l'asso, bravo" e finisce lì. Quello che avete davanti è un simulatore equo e professionale, con punteggio aggiornato in tempo reale, in modo da poter decidere se "rischiare" o meno coscientemente. Sembra strano a dirsi, ma è Hard Core, perché si ha in ogni istante il potere di cambiare in ogni istante l'andamento della parita coscientemente con le proprie scelte.

## Come ricompilare
Per prima cosa occorre ricompilare la libreria cardframework.uwp.
Poi, una volta ottenuta la DLL, bisogna importarla nel progetto, non si può usare nuget perché è nato dopo.
Bisogna cliccare col tasto destro su riferimenti e quindi aggiungi riferimento, poi sfoglia, e selezionare la dll.

## Bug noti

Su Xbox, per cambiare linguaggio bisogna disinstallare e reinstallare l'applicativo.

## Come installare

## Come aprire un account da sviluppatore xbox

Andate su https://developer.microsoft.com/it-it/microsoft-store/register/ e registrate il vostro account hotmail (possibilmente non quello principale, ma createne uno nuovo) come developer. Pagate i 17 euro una tantum, ed aspettate l'attivazione, dopodiché aprite sulla vostra SECONDA (non di gioco) xbox one o series x l'app store e scaricate l'app dev kit (quella con le icone della series s/x, non quella con l'icona della one), apritela e seguite le istruzioni a video, facendo attenzione a quando vi dice "aprite windows 10 aprite, aprite aka.ms/quello_che_è e registrate la xbox), ad aprire da pc nel vostro account developer microsoft la sezione "xbox one development consoles", premere il tasto "+" in alto a destra, selezionare "enter activation code" ed indicare il codice mostrato a video; quando l'impostazione è finita la console viene riavviata e si entra nel developer mode.

## The new fashion way

Aprite il devhome della vostra xbox one, nella sezione remote access configurate user e password, e poi apritela col browser.
In Home, sezione My Apps and Games, selezionare ADD, ed indicare il package principale (msixbundle) della release scelta in deploy or install application.
Le dipedenze sono gli appx.

## The old fashion way
Per prima cosa registrate il vostro account di developer nell'apposita sezione in alto a destra, dopo cliccate su "show visual studio pin" e salvatevelo, ora pasate al pc, aprite il progetto, cliccate su dispositivo remoto invece che su computer locale quando dovete compilare, indicate l'ip della xbox, aspettate che vi chiede il pin, inseritelo e compilate.

A breve nella sezione bassa centrale comparirà il programma, basta avviarla per poter giocare.


## Come installarlo sul surface

Il package è sia per arm64 che per amd64, è sufficiente registrare la chiave nel sistema, non nell'account utente, nella sezione persone attendibili per installare l'msixbundle ed ottenere l'app.
Può essere usata per creare chioschi.


## Screenshot dell'app

![Screenshot_2025-01-12_12-08-26](https://github.com/user-attachments/assets/3988503e-92c1-416e-8a15-d4de54beb147)


## Video dell'applicazione

https://1drv.ms/v/c/7413c8764c078e99/Eb879Dfosx9PhBQ7SqZfMeYB65eD9iiAFRuBoyUqZaaz2Q?e=QzJY2Y

## Donazione

http://numerone.altervista.org/donazioni.php
