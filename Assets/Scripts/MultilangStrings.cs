using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultilangStrings : MonoBehaviour
{
    public static MultilangStrings Instance;
    public List<string> mainMenuEngStrings = new List<string>();
    public List<string> mainMenuSwedStrings = new List<string>();
    public List<string> settingsEngStrings = new List<string>();
    public List<string> settingsSwedStrings = new List<string>();
    public List<string> tacticCardTitleEngStrings = new List<string>();
    public List<string> tacticCardTitleSwedStrings = new List<string>();
    public List<string> tacticCardDescriptionEngStrings = new List<string>();
    public List<string> tacticCardDescriptionSwedStrings = new List<string>();
    public List<string> bonusCardTitleEngStrings = new List<string>();
    public List<string> bonusCardTitleSwedStrings = new List<string>();
    public List<string> bonusCardDescriptionEngStrings = new List<string>();
    public List<string> bonusCardDescriptionSwedStrings = new List<string>();
    public List<string> othersEngStrings = new List<string>();
    public List<string> othersSwedStrings = new List<string>();
    public List<string> demoEngStrings = new List<string>();
    public List<string> demoSwedStrings = new List<string>();


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMenuEngStrings.Add("PLAYER SCHOOL RED");
        mainMenuEngStrings.Add("PLAYER SCHOOL ORANGE");
        mainMenuEngStrings.Add("SINGLES");
        mainMenuEngStrings.Add("DOUBLES");
        mainMenuEngStrings.Add("SETTINGS");
        mainMenuEngStrings.Add("SINGLES");
        mainMenuEngStrings.Add("LOCAL PLAY");
        mainMenuEngStrings.Add("VS A.I");
        mainMenuEngStrings.Add("ONLINE");

        mainMenuSwedStrings.Add("Player School Röd");
        mainMenuSwedStrings.Add("Player School Orange");
        mainMenuSwedStrings.Add("Singel");
        mainMenuSwedStrings.Add("Dubbel");
        mainMenuSwedStrings.Add("Inställningar");
        mainMenuSwedStrings.Add("Singel");
        mainMenuSwedStrings.Add("Lokalt");
        mainMenuSwedStrings.Add("Mot datorn");
        mainMenuSwedStrings.Add("Online");

        settingsEngStrings.Add("LANGUAGES");
        settingsEngStrings.Add("PLAYER SETTINGS");
        settingsEngStrings.Add("GAME PLAY");
        settingsEngStrings.Add("MECHANICS");
        settingsEngStrings.Add("English");
        settingsEngStrings.Add("Swedish");
        settingsEngStrings.Add("Player1 name");
        settingsEngStrings.Add("Player2 name");
        settingsEngStrings.Add("Player1 color");
        settingsEngStrings.Add("Player2 color");
        settingsEngStrings.Add("Roll animations");
        settingsEngStrings.Add("Aim grid");
        settingsEngStrings.Add("Special strokes");
        settingsEngStrings.Add("Bonus cards");
        settingsEngStrings.Add("Tactic cards");
        settingsEngStrings.Add("Tactic goal");                
        settingsEngStrings.Add("On");
        settingsEngStrings.Add("Off");
        settingsEngStrings.Add("On");
        settingsEngStrings.Add("Off");
        settingsEngStrings.Add("On");
        settingsEngStrings.Add("Off");
        settingsEngStrings.Add("On");
        settingsEngStrings.Add("Off");
        settingsEngStrings.Add("LANGUAGES");
        settingsEngStrings.Add("PLAYER SETTINGS");
        settingsEngStrings.Add("GAME PLAY");
        settingsEngStrings.Add("MECHANICS");
        settingsEngStrings.Add("SHOW DEMO");

        settingsSwedStrings.Add("Språk");
        settingsSwedStrings.Add("Spelare inställningar");
        settingsSwedStrings.Add("Spelet");
        settingsSwedStrings.Add("Funktioner");
        settingsSwedStrings.Add("Engelska");
        settingsSwedStrings.Add("Svenska");
        settingsSwedStrings.Add("Spelare1 Namn");
        settingsSwedStrings.Add("Spelare2 Namn");
        settingsSwedStrings.Add("Spelare1 Färg");
        settingsSwedStrings.Add("Spelare2 Färg");        
        settingsSwedStrings.Add("Bollanimationer");
        settingsSwedStrings.Add("Rutnät för att sikta");
        settingsSwedStrings.Add("Specialslag");
        settingsSwedStrings.Add("Bonuskort");
        settingsSwedStrings.Add("Taktikkort");
        settingsSwedStrings.Add("Taktikmål");
        settingsSwedStrings.Add("På");
        settingsSwedStrings.Add("Av");
        settingsSwedStrings.Add("På");
        settingsSwedStrings.Add("Av");
        settingsSwedStrings.Add("På");
        settingsSwedStrings.Add("Av");
        settingsSwedStrings.Add("På");
        settingsSwedStrings.Add("Av");
        settingsSwedStrings.Add("Språk");
        settingsSwedStrings.Add("Spelare inställningar");
        settingsSwedStrings.Add("Spelet");
        settingsSwedStrings.Add("Funktioner");
        settingsSwedStrings.Add("VISA DEMO");


        tacticCardTitleEngStrings.Add("TO THE NET");
        tacticCardTitleEngStrings.Add("ON THE DEFENSIVE");
        tacticCardTitleEngStrings.Add("ACCURATE FINISHING");
        tacticCardTitleEngStrings.Add("ON THE OFFENSIVE");
        tacticCardTitleEngStrings.Add("SMART PLAY");

        tacticCardTitleSwedStrings.Add("Volleyspel");
        tacticCardTitleSwedStrings.Add("Defensivt spel");
        tacticCardTitleSwedStrings.Add("Precisionspel");
        tacticCardTitleSwedStrings.Add("Offensivtspel");
        tacticCardTitleSwedStrings.Add("Smartspel");

        tacticCardDescriptionEngStrings.Add("WIN A POINT WITH YOUR FINAL STROKE AS A VOLLEY.");
        tacticCardDescriptionEngStrings.Add("COMPLETE A RALLY OF 10+ STROKES." + '\n');
        tacticCardDescriptionEngStrings.Add("WIN AND FINISH A POINT WITH A STROKE LANDING BESIDE A OUTER LINE.");
        tacticCardDescriptionEngStrings.Add("WIN A RALLY OF 5 STROKES OR LESS." + '\n');
        tacticCardDescriptionEngStrings.Add("WIN A POINT WITHOUT USING ANY SPECIAL STROKES.");

        tacticCardDescriptionSwedStrings.Add("Vinn ett poäng när ditt sista slag är en volley.");
        tacticCardDescriptionSwedStrings.Add("Genomföra en rally på 10+ slag.");
        tacticCardDescriptionSwedStrings.Add("Vinn och avsluta ett poäng med ett slag som studsar vid en ytterlinje.");
        tacticCardDescriptionSwedStrings.Add("Vinn en rally på 5 eller mindre slag.");
        tacticCardDescriptionSwedStrings.Add("Vinn ett poäng utan att använda specialslag.");

        bonusCardTitleEngStrings.Add("No look");
        bonusCardTitleEngStrings.Add("Grand slam");
        bonusCardTitleEngStrings.Add("Technically Gifted");
        bonusCardTitleEngStrings.Add("Survivor");
        bonusCardTitleEngStrings.Add("New racket");
        bonusCardTitleEngStrings.Add("Precision");
        bonusCardTitleEngStrings.Add("Tweener");
        bonusCardTitleEngStrings.Add("Athletic");
        bonusCardTitleEngStrings.Add("Change sides");
        bonusCardTitleEngStrings.Add("Coach advice");
        bonusCardTitleEngStrings.Add("Entertainment");
        bonusCardTitleEngStrings.Add("Home support");

        bonusCardTitleSwedStrings.Add("No look");
        bonusCardTitleSwedStrings.Add("Grand slam");
        bonusCardTitleSwedStrings.Add("Teknikbegåvad");
        bonusCardTitleSwedStrings.Add("Överlevnad");
        bonusCardTitleSwedStrings.Add("Nytt rack");
        bonusCardTitleSwedStrings.Add("Precision");
        bonusCardTitleSwedStrings.Add("Tweener");
        bonusCardTitleSwedStrings.Add("Atlet");
        bonusCardTitleSwedStrings.Add("Byt sida");
        bonusCardTitleSwedStrings.Add("Tips från coachen");
        bonusCardTitleSwedStrings.Add("Underhållande");
        bonusCardTitleSwedStrings.Add("Hempublik");

        bonusCardDescriptionEngStrings.Add("You pulled off a no-look shot which has thrown your opponent off their game. ");
        bonusCardDescriptionEngStrings.Add("The next game looks to be deciding. ");
        bonusCardDescriptionEngStrings.Add("Your hours working on court are paying off. your strokes are silky smooth. ");
        bonusCardDescriptionEngStrings.Add("You pulled off an amazing stretch to get to that ball but seem to have hurt yourself.");
        bonusCardDescriptionEngStrings.Add("Your racket broke and you replace it with a new one which needs getting use to.");
        bonusCardDescriptionEngStrings.Add("You’re in the zone and everythings going your way.");
        bonusCardDescriptionEngStrings.Add("In the last game you pulled off a masterful tweener. The crowd are on their feet. ");
        bonusCardDescriptionEngStrings.Add("You don’t know how to give up. You’re moving excelently.");
        bonusCardDescriptionEngStrings.Add("You won the last game. change sides. if the game score is even, win an extra game.");
        bonusCardDescriptionEngStrings.Add("Your coach has given you useful feedback. gain 2 markers on a tactics card of your choice.");
        bonusCardDescriptionEngStrings.Add("You're playing some truly great tennis. ");
        bonusCardDescriptionEngStrings.Add("You’ve got an amazing fan base cheering you on. For every friend in the room start with 1 point in the next game.");

        bonusCardDescriptionSwedStrings.Add("Du titat åt ena hållet och slår mot andra. Motståndaren tappar det totalt.");
        bonusCardDescriptionSwedStrings.Add("Nästa game ser avgörande ut.");
        bonusCardDescriptionSwedStrings.Add("Dina timmar på banan lönar sig med tekniken. Du behöver knappt anstränga dig");
        bonusCardDescriptionSwedStrings.Add("På något sätt lyckades du nå bollen men verkar ha skadat dig.");
        bonusCardDescriptionSwedStrings.Add("Ditt rack gick sönder och du har ersatt den med en ny som du får vänja dig vid.");
        bonusCardDescriptionSwedStrings.Add("Du har hittat rytmen och allting funkar bra");
        bonusCardDescriptionSwedStrings.Add("I det förra gamet klarade du en tweener. Publiken blir tokig!");
        bonusCardDescriptionSwedStrings.Add("VDu ger aldrig upp och rör dig gallant.");
        bonusCardDescriptionSwedStrings.Add("Byt sida med motståndaren. Om gameställningen är ett jämt tal vinn ett extra game.");
        bonusCardDescriptionSwedStrings.Add("Din coach har gett dig värdefull feedback.");
        bonusCardDescriptionSwedStrings.Add("Ni spelar otroligt bra tennis! ");
        bonusCardDescriptionSwedStrings.Add("Du har mycket stöd bakom dig. för varje kompis i rummet får du ett poäng i nsäta game.");

        othersEngStrings.Add("Player 1 Please choose a tactic card");
        othersEngStrings.Add("Player 2 Please choose a tactic card");
        othersSwedStrings.Add("Spelare 1: Välj ett taktikkort");
        othersSwedStrings.Add("Spelare 2: Välj ett taktikkort");

        demoEngStrings.Add("DEUCE IS A TENNIS GAME BASED ON SKILL AND LUCK... LETS TAKE A LOOK AROUND...");
        demoEngStrings.Add("THE GAME BOARD IS DIVIDED IN ZONES AND SQUARES...");
        demoEngStrings.Add("DEPENDING ON THE ZONE YOU ARE IN, YOU CAN MOVE X NUMBER OF SQUARES...");
        demoEngStrings.Add("THE SCORE BOARD SHOWS THE PLAYERS’ NAMES, WHO IS SERVING, SET SCORES AND GAME SCORES...");
        demoEngStrings.Add("EACH POINT STARTS BY CHOOSING A TACTIC CARD... COMPLETE THE TACTIC CARD GOAL TO GAIN A MARKER...");
        demoEngStrings.Add("GO FOR THE SMART PLAY TACTIC... TO COMPLETE THE GOAL WE NEED TO WIN A POINT WITHOUT SPECIAL STROKES...");
        demoEngStrings.Add("CHOOSE YOUR SERVING POSITION... THE UMPIRE WILL TELL YOU WHAT PHASE IT IS…");
        demoEngStrings.Add("THE UMPIRE WILL TELL YOU WHAT PHASE IT IS…");        
        demoEngStrings.Add("EVERY HIT REQUIRES A ROLL... 1-9 AND NET ARE POSSIBLE OUTCOMES...");
        demoEngStrings.Add("YOU HIT THE NET.. UNLUCKY! SECOND SERVE INCOMING!");
        demoEngStrings.Add("EVERY HIT REQUIRES A ROLL... 1-9 AND NET ARE POSSIBLE OUTCOMES...");
        demoEngStrings.Add("GREAT SERVE! YOU HIT A 2. THE BALL LANDS IN THAT SQUARE…");
        demoEngStrings.Add("AS MARTINA IS IN ZONE 5. SHE CAN MOVE 5 STEPS TO THE BALL.");
        demoEngStrings.Add("GREEN SQUARES REPRESENT POSSIBLE SPACES TO HIT THE BALL…");
        demoEngStrings.Add("MARTINA RUNS TO HIT AND TAKES AIM…");
        demoEngStrings.Add("MARTINA TAKES A CHANCE AND AIMS FOR THE LINE CROSS-COURT... 7, 8 AND 9 ARE OUT");
        demoEngStrings.Add("MARTINA HITS AN 8 AND THE BALL GOES OUT... 15-0!");
        demoEngStrings.Add("GREAT JOB! YOU EARNED A MARKER ON YOUR SMART PLAY TACTIC…");
        demoEngStrings.Add("LETS CHOOSE IT AGAIN, IT WORKED WELL LAST TIME...");
        demoEngStrings.Add("DOUBLE FAULT! THE CLOSER YOU AIM TO THE LINES THE HIGHER THE RISK...");
        demoEngStrings.Add("LET’S TRY SOMETHING DIFFERENT. CHOOSE OFFENSIVE PLAY. WE NEED TO WIN THE POINT QUICKLY! (<5 STROKES)");
        demoEngStrings.Add("LOOK AT MARTINA’S POSITION.. IF WE HIT AN ANGLED SERVE HERE WE CAN HIT AN ACE…");
        demoEngStrings.Add("GREAT SERVE! SHE CAN'T REACH IT WITH 5 STEPS. ACE! 30-15");
        demoEngStrings.Add("LET’S GO AGAIN! WE’RE ON A ROLL HERE…");
        demoEngStrings.Add("AFTER HITTING THE BALL, YOU MAY RECOVER X STEPS DEPENDING ON THE ZONE YOU HIT THE BALL…");
        demoEngStrings.Add("MARTINA HITS CROSS-COURT HAS MOVED 4 STEPS BACK INTO THE COURT.");
        demoEngStrings.Add("YOU SEE AN OPENING ON MARTINA’S FOREHAND SIDE AND TAKE THE BALL EARLY");
        demoEngStrings.Add("LET’S OPEN UP OUR SPECIAL STROKES");
        demoEngStrings.Add("WE WANT TO SPEED UP PLAY. POWER SHOT. MARTINA WILL MOVE 1 STEP LESS & SO WILL WE.");
        demoEngStrings.Add("LET’S FINISH THIS GAME UP!");
        demoEngStrings.Add("GREAT JOB! YOU WON THE GAME. YOU RECEIVE A BONUS IN THE NEXT GAME.");
        demoEngStrings.Add("GOOD LUCK OUT THERE!");

        demoSwedStrings.Add("DEUCE ÄR ETT TENNIS SPEL BASERAT PÅ SKICKLIGHET OCH TUR… VI SKA TA EN TITT RUNT…");
        demoSwedStrings.Add("SPELBRÄDAN ÄR UPPDELAT PÅ OLIKA ZONER OCH RUTOR…");
        demoSwedStrings.Add("BEROENDE PÅ VILKEN ZON DU STÅR I KAN DU RÖRA DIG X ANTAL RUTOR…");
        demoSwedStrings.Add("POÄNGBRÄDAN VISAR SPELARNAS NAMN, VEM SOM SERVAR, OCH VAD DET STÅR I GAMET OCH SET…");
        demoSwedStrings.Add("VARJE POÄNG BÖRJAR GENOM ATT VÄLJA ETT TAKTIK KORT… LYCKAS DU MED DITT UPPDRAG FÅR DU EN MARKÖR…");
        demoSwedStrings.Add("VÄLJ SMARTSPEL TAKTIKEN… FÖR ATT LYCKAS MED UPPDRAGET BEHÖVER VI VINNA ETT POÄNG UTAN SPECIAL SLAG…");
        demoSwedStrings.Add("VÄLJ DIN SERVE POSITION… DOMAREN KOMMER BERÄTTA DIG VAD DU SKA GÖRA…");        
        demoSwedStrings.Add("SIKTA FÖR DEN BLÅA RUTAN I MITTEN…");
        demoSwedStrings.Add("I SAMBAND MED VARJE SLAG KOMMER EN TÄRNING ATT SLÅS… 1-9 OCH NÄT ÄR MÖJLIGA RESULTAT…");
        demoSwedStrings.Add("DU TRÄFFADE NÄTET… OTUR! ANDRA SERVE…");
        demoSwedStrings.Add("I SAMBAND MED VARJE SLAG KOMMER EN TÄRNING ATT SLÅS… 1-9 OCH NÄT ÄR MÖJLIGA RESULTAT…");
        demoSwedStrings.Add("GRYM SERVE! DU SLOG EN 2:A… BOLLEN LANDAR I 2:ANS RUTA…");
        demoSwedStrings.Add("SOM MARTINA ÄR I ZON 5. HAN KAN RÖRA 5 STEG TILL BOLLEN.");
        demoSwedStrings.Add("GRÖNA RUTOR VISAR VILKA MÖJLIGA RUTOR DU KAN TRÄFFA BOLLEN…");
        demoSwedStrings.Add("MARTINA SPRINGER FÖR ATT SLÅR BOLLEN OCH SIKTAR…");
        demoSwedStrings.Add("MARTINA GÖR EN CHANSNING OCH SIKTAR PÅ LINJEN MED ETT CROSS SLAG… 7, 8 OCH 9 ÄR UT…");
        demoSwedStrings.Add("MARTINA SLÅR EN 8 OCH BOLLEN GÅR UT.. 15-0!");
        demoSwedStrings.Add("HÄRLIGT! DU FICK EN MARKÖR PÅ SMARTSPEL TAKTIKEN…");
        demoSwedStrings.Add("VÄLJ SMARTSPEL IGEN, DET FUNGERADE BRA SIST…");
        demoSwedStrings.Add("DUBBEL FEL! SIKTAR DU VID LINJEN FINNS DET ALLTID EN RISK…");
        demoSwedStrings.Add("VI PROVAR NÅGOT ANNAT… VÄLJ OFFENSIVT SPEL… VI BEHÖVER VINNA BOLLEN SNABBT! (<5 SLAG)");
        demoSwedStrings.Add("TITTA PÅ MARTINAS POSITION… SLÅR VI EN VINKLAD SERVE KAN VI SLÅ ETT ESS…");
        demoSwedStrings.Add("STOR SERVER! HON KAN INTE NÅ DET MED 5 STEG. ESS! 30-15");
        demoSwedStrings.Add("VI KÖR IGEN! DET GÅR BRA DETTA…");
        demoSwedStrings.Add("EFTER ATT DU SLÅR BOLLEN FÅR DU ÅTERHÄMTA DIG X RUTOR BEROENDE PÅ ZONEN DU TRÄFFAR BOLLEN I…");
        demoSwedStrings.Add("MARTINA HITS CROSS-COURT HAR FLYTT 4 STEG TILLBAKA TILL DOMSTOLEN.");
        demoSwedStrings.Add("DU SER EN LUCKA PÅ MARTINAS FOREHAND OCH TAR BOLLEN TIDIGT…");
        demoSwedStrings.Add("TRYCK PÅ SPECIALSLAG…");
        demoSwedStrings.Add("VI VILL ÖKA TEMPOT PÅ SPELET… VÄLJ KRAFT… DU OCH MARTINA KOMMER FÅ RÖRA SIG 1 RUTA MINDRE");
        demoSwedStrings.Add("VI FÅR AVSLUTA GAMET!");
        demoSwedStrings.Add("SUPER! DU VANN GAMET… DU FÅR EN BONUS I NÄSTA GAME…");
        demoSwedStrings.Add("LYCKA TILL MED FORTSÄTTNINGEN!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
