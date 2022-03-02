using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private delegate void DELEGATE(); //main threadחדש, וזה מאפשר לי לקרוא למשתנים מה thread יצירת טיימר יוצר  

        System.Timers.Timer atime = new System.Timers.Timer(); //טיימר שמוגדר לו אינטרבל, שבסופו פעולה מסוימת מופעת
                                                               //השתמשתי בו כדי לגרום לתשובה להבהב בצבע הנכון והצעד הבא בעקבותיו
        int ticks = 6; //form משתנה עזר לטיימר השני, מוגדר בתוך ה
                       // משמש כטיימר של שש שניות לכל שאלה 

        Button B; //משתנה עזר 
        int location;//משתנה עזר
        int it = 0;//משתנה עזר 
        bool flag; //משתנה עזר בוליאני
        int num = 0;
        SoundPlayer correct = new SoundPlayer("correct.wav");
        SoundPlayer wrongAns = new SoundPlayer("wrongly.wav");

        int attempts = 0; //לשחקן יש 3 חיים, סופר את הכשלונות
        int Counter = 1; // סופר את מספר השאלות על מנת הצגה
        int points = 0;//הנקודות של השחקן

        string[,] UnitOne = new string[153, 2]; //מערך של מילים ופירושים
        Button[] buttons = new Button[4];//מערך של ארבעת הכפתורים על מנת להגריל אותם
        List<int> randoms = new List<int>(); // רשימה שמוכנסים לתוכה מספרים שהוגרלו, על מנת שלא יחזרו על עצמם
        List<int> questions = new List<int>();
        int x = 0;
        int y = 0;
        public Form1()
        {
            InitializeComponent();

            atime.Interval = 1500;
            atime.Elapsed += Atime_Elapsed; //יצירת הפעולה אליו נשלחת התוכנית כל פעם שהטיימר מגיע ל2 שניות

            button1.Hide();
            button2.Hide();
            button3.Hide();
            button4.Hide();
            Start.Hide();
            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;

            UnitOne[0, 0] = "אבד את העשתונות";
            UnitOne[0, 1] = "הפסיק לחשוב בהיגיון, התבלבל";

            UnitOne[1, 0] = "אדן";
            UnitOne[1, 1] = "יסוד בסיס, הדבר שעליו דבר מה מתבסס. קורת עץ המונחת מתחת לפסי הרכבת";

            UnitOne[2, 0] = "אחרית";
            UnitOne[2, 1] = "סוף, התקופה האחרונה";

            UnitOne[3, 0] = "אלומה";
            UnitOne[3, 1] = "אוסף קרניים, צרור שיבולים";

            UnitOne[4, 0] = "אנקה";
            UnitOne[4, 1] = "צעקה מתוך צער או כאב";

            UnitOne[5, 0] = "אצטבה";
            UnitOne[5, 1] = "רף,מדף";

            UnitOne[6, 0] = "בה בעת";
            UnitOne[6, 1] = "באותו זמן, באותה שעה";

            UnitOne[7, 0] = "ביבר";
            UnitOne[7, 1] = "גן חיות";

            UnitOne[8, 0] = "את";
            UnitOne[8, 1] = "כלי עבודה המשמש לחפירה";

            UnitOne[9, 0] = "בלימה";
            UnitOne[9, 1] = "עצירה, מחסום";

            UnitOne[10, 0] = "בער";
            UnitOne[10, 1] = "חסר השכלה, חסר דעת, בור";

            UnitOne[11, 0] = "ברא";
            UnitOne[11, 1] = "כרת עצים לשם פינוי שטח";

            UnitOne[12, 0] = "גדיד";
            UnitOne[12, 1] = "קטיף תמרים";

            UnitOne[13, 0] = "גזר";
            UnitOne[13, 1] = "חתיכה, חלק";

            UnitOne[14, 0] = "גזרה";
            UnitOne[14, 1] = "תקנה או פקודה שיש בה איסור";

            UnitOne[15, 0] = "גמלוני";
            UnitOne[15, 1] = "גדול, ארוך ומסורבל";

            UnitOne[16, 0] = "גת";
            UnitOne[16, 1] = "בור בו דורכים ענבים";

            UnitOne[17, 0] = "דובב";
            UnitOne[17, 1] = "גרם, שכנע מישהו לדבר";

            UnitOne[18, 0] = "דלה";
            UnitOne[18, 1] = " העלה דברים ממעמקי הים. חילץ פרטי מידע, שלף משמעויות נסתרות. העלה מים מן הבאר";

            UnitOne[19, 0] = "הבליג";
            UnitOne[19, 1] = "התאפק, ריסן את עצמו, נמנע מלהגיב";

            UnitOne[20, 0] = "החניף";
            UnitOne[20, 1] = "שיבח מישהו בפניו כדי למצוא חן בעיניו או כדי להשיג דבר מה";

            UnitOne[21, 0] = "היקוות";
            UnitOne[21, 1] = "האספות מים";

            UnitOne[22, 0] = "הלעיז";
            UnitOne[22, 1] = "אמר, כתב או פרסם דברים על מישהו, לרוב שיקריים, הפוגעים בשמו הטוב";

            UnitOne[23, 0] = "הסה";
            UnitOne[23, 1] = "השתיק, הורה לשתוק";

            UnitOne[24, 0] = "העפיל";
            UnitOne[24, 1] = "עלה טיפס, קודם למעמד, עלה שלב";

            UnitOne[25, 0] = "הצר";
            UnitOne[25, 1] = "עשה שיהיה צר יותר, הצטער על משהו";

            UnitOne[26, 0] = "השבית";
            UnitOne[26, 1] = "הורה לא להפעיל משהו, הפסיק לזמן מה את הפעילות";

            UnitOne[27, 0] = "השתרך";
            UnitOne[27, 1] = "הלך באיטיות ובכבדות, נגרר נסחב בעקבות מישהו או משהו";

            UnitOne[28, 0] = "התוהלל";
            UnitOne[28, 1] = "התנהג בצורה פרועה, מופקרת ולא מוסרית, בעיקר מבחינה מינית";

            UnitOne[29, 0] = "התנצח";
            UnitOne[29, 1] = "התווכח עם מישהו בתוקפנות ועיקשות, התכתש מילולית, לרוב בשל אמונות או דעות מנוגדות";

            UnitOne[30, 0] = "היתרה";
            UnitOne[30, 1] = "הזהיר, איים";

            UnitOne[31, 0] = "זג";
            UnitOne[31, 1] = "קליפת הענב";

            UnitOne[32, 0] = "זרה";
            UnitOne[32, 1] = "פיזר,הפיץ";

            UnitOne[33, 0] = "חזר לסורו";
            UnitOne[33, 1] = "שב למעשיו הרעים";

            UnitOne[34, 0] = "חמלה";
            UnitOne[34, 1] = "רחמים";

            UnitOne[35, 0] = "חרון";
            UnitOne[35, 1] = "כעס";

            UnitOne[36, 0] = "טבור";
            UnitOne[36, 1] = "מרכז/ פופיק";

            UnitOne[37, 0] = "יאות";
            UnitOne[37, 1] = "טוב ויפה, מתאים";

            UnitOne[38, 0] = "יצא מכליו";
            UnitOne[38, 1] = "התרתח מאוד, התפרץ";

            UnitOne[39, 0] = "ישמון";
            UnitOne[39, 1] = "שממה, מדבר";

            UnitOne[40, 0] = "כורח";
            UnitOne[40, 1] = "חוסר ברירה";

            UnitOne[41, 0] = "כמוש";
            UnitOne[41, 1] = "נבול, בלוי, מצומק";

            UnitOne[42, 0] = "כפת";
            UnitOne[42, 1] = "קשר היטב דבר, בעיקר ידיים ורגליים זו לזו";

            UnitOne[43, 0] = "לבל";
            UnitOne[43, 1] = "פן, שמא";

            UnitOne[44, 0] = "למזער";
            UnitOne[44, 1] = "לכל הפחות";

            UnitOne[45, 0] = "לפת";
            UnitOne[45, 1] = "תפס ואחז בכוח";

            UnitOne[46, 0] = "מבודר";
            UnitOne[46, 1] = "מפוזר";

            UnitOne[47, 0] = "מגרעת";
            UnitOne[47, 1] = "חיסרון, פגם / שקע, גומחה";

            UnitOne[48, 0] = "מהל";
            UnitOne[48, 1] = "ערבב נוזל בנוזל אחר, בעיקר מים";

            UnitOne[49, 0] = "מורא";
            UnitOne[49, 1] = "פחד רב, אימה";

            UnitOne[50, 0] = "מטבל";
            UnitOne[50, 1] = " דיפ, רוטב";

            UnitOne[51, 0] = "מיוער";
            UnitOne[51, 1] = "מכוסה יער, שצומחים עליו עצי יער";

            UnitOne[52, 0] = "מליץ";
            UnitOne[52, 1] = "מתורגמן / ממליץ, מסנגר";

            UnitOne[53, 0] = "מסואב";
            UnitOne[53, 1] = "מלוכלך, מזוהם, בזוי, מושחת";

            UnitOne[54, 0] = "מפורז";
            UnitOne[54, 1] = "מפורק מנשקו";

            UnitOne[55, 0] = "מצנפת";
            UnitOne[55, 1] = "כובע העשוי בד העוטף ומקיף את הראש";

            UnitOne[56, 0] = "משאב";
            UnitOne[56, 1] = "מקור, עתודה, כלל האמצעים העומדים לרשות גורם מסוים";

            UnitOne[57, 0] = "משלח יד";
            UnitOne[57, 1] = "עבודה, מקצוע, עיסוק";

            UnitOne[58, 0] = "נאלח";
            UnitOne[58, 1] = "מלוכלך, מזוהם, בזוי, מושחת";

            UnitOne[59, 0] = "נגיש";
            UnitOne[59, 1] = "שאפשר לגשת אליו";

            UnitOne[60, 0] = "נואל";
            UnitOne[60, 1] = "אווילי, טיפשי";

            UnitOne[61, 0] = "נחה דעתו";
            UnitOne[61, 1] = "נרגע, בא על סיפוקו";

            UnitOne[62, 0] = "נטר";
            UnitOne[62, 1] = "שמר, שמר טינה, איבה";

            UnitOne[63, 0] = "נכלם";
            UnitOne[63, 1] = "נבוך, מבויש";

            UnitOne[64, 0] = "נסק";
            UnitOne[64, 1] = "עלה, גדל, האמיר, ניתק מהקרקע והתרומם לאוויר";

            UnitOne[65, 0] = "נקעה נפשו";
            UnitOne[65, 1] = " -מאס מ-, בחל ב";

            UnitOne[66, 0] = "נתין";
            UnitOne[66, 1] = "אזרח";

            UnitOne[67, 0] = "סהר";
            UnitOne[67, 1] = "ירח";

            UnitOne[68, 0] = "סך";
            UnitOne[68, 1] = "משח, מרח בשמן";

            UnitOne[69, 0] = "סיפח";
            UnitOne[69, 1] = "צירף, חיבר, הוסיף דבר אל דבר";

            UnitOne[70, 0] = "עדי";
            UnitOne[70, 1] = "תכשיט";

            UnitOne[71, 0] = "צדיה";
            UnitOne[71, 1] = "כוונה רעה, זדון, מזימה";

            UnitOne[72, 0] = "פרקדן";
            UnitOne[72, 1] = "שכוב על הגב";

            UnitOne[73, 0] = "פסיקה";
            UnitOne[73, 1] = "מתן פסק דין";

            UnitOne[74, 0] = "פך";
            UnitOne[74, 1] = "כלי קטן לנוזלים";

            UnitOne[75, 0] = "פושט יד";
            UnitOne[75, 1] = "עני המבקש נדבות";

            UnitOne[76, 0] = "עתיר";
            UnitOne[76, 1] = "עשיר,רב";

            UnitOne[77, 0] = "ערובה";
            UnitOne[77, 1] = "ביטחון לכך שמשהו יתקיים או יבוצע כמובטח";

            UnitOne[78, 0] = "עני מרוד";
            UnitOne[78, 1] = "עני ביותר";

            UnitOne[79, 0] = "עלטה";
            UnitOne[79, 1] = "חושך אפלה";

            UnitOne[80, 0] = "עילאי";
            UnitOne[80, 1] = "עליון, נעלה, נאצל";

            UnitOne[81, 0] = "תרעומת";
            UnitOne[81, 1] = "תחושת רוגז, טינה, כעס";

            UnitOne[82, 0] = "תעה";
            UnitOne[82, 1] = "שוטט בחיפוש הדרך, איבד את הדרך / ססה מן הדרך המוסרית";

            UnitOne[83, 0] = "תחינה";
            UnitOne[83, 1] = "תפילת בקשה";

            UnitOne[84, 0] = "תהה על קנקנו";
            UnitOne[84, 1] = "בדק את טיבו, חקר את מהותו";

            UnitOne[85, 0] = "שררה";
            UnitOne[85, 1] = "שלטון,ממשלה";

            UnitOne[86, 0] = "שעיר לעזאזל";
            UnitOne[86, 1] = "כינוי למי שמשמש קורבן לחטאי אחרים למרות היותו חף מפשע";

            UnitOne[87, 0] = "שם פעמיו אל";
            UnitOne[87, 1] = "יצא לדרך, החל להתקדם אל";

            UnitOne[88, 0] = "שטנה";
            UnitOne[88, 1] = "דברי איבה, והאשמה";

            UnitOne[89, 0] = "שדוף";
            UnitOne[89, 1] = "נבוב, חסר תוכן, ריק";

            UnitOne[90, 0] = "רשף";
            UnitOne[90, 1] = "ניצוץ, גץ";

            UnitOne[91, 0] = "צחיח";
            UnitOne[91, 1] = "יבש, מחוסר במים";

            UnitOne[92, 0] = "צניפה";
            UnitOne[92, 1] = "צהלת הסוס";

            UnitOne[93, 0] = "קבס";
            UnitOne[93, 1] = "תחושת גועל, בחילה עד כדי הקאה";
       
            UnitOne[94, 0] = "קורת רוח";
            UnitOne[94, 1] = "שביעות רצון, הנאה, נחת רוח";

            UnitOne[95, 0] = "קלוש";
            UnitOne[95, 1] = "דליל, חלש, רופף";

            UnitOne[96, 0] = "קסת";
            UnitOne[96, 1] = "כלי קיבול לדיו";

            UnitOne[97, 0] = "קירטע";
            UnitOne[97, 1] = "בתקדם בחוסר יציבות, תוך נדנודים וקפיצות. לרוב עקב צליעה";

            UnitOne[98, 0] = "רידד";
            UnitOne[98, 1] = "שיטח, הפך דבר מה לשטוח ודק";

            UnitOne[99, 0] = "רז";
            UnitOne[99, 1] = "סוד";

            UnitOne[100, 0] = "רעלה";
            UnitOne[100, 1] = "צעיף נשים לכיסוי הפנים";

            UnitOne[101, 0] = "אבה";
            UnitOne[101, 1] = "רצה, הסכים";

            UnitOne[102, 0] = "אדרבה";
            UnitOne[102, 1] = "ההפך הוא הנכון";

            UnitOne[103, 0] = "אידוי";
            UnitOne[103, 1] = "הפיכת נוזלים לגז";

            UnitOne[104, 0] = "אלונטית";
            UnitOne[104, 1] = "מגבת";

            UnitOne[105, 0] = "אנקול";
            UnitOne[105, 1] = "וו תליה";

            UnitOne[106, 0] = "אצטלה";
            UnitOne[106, 1] = "גלימה";

            UnitOne[107, 0] = "בא בימים";
            UnitOne[107, 1] = "זקן";

            UnitOne[108, 0] = "בוטה";
            UnitOne[108, 1] = "חריף,נוקב";

            UnitOne[109, 0] = "ביית";
            UnitOne[109, 1] = "סיגל בעל חיים לחיות בקרבת אדם, לתועלת האדם";

            UnitOne[110, 0] = "בלית ברירה";
            UnitOne[110, 1] = "כשאין אפשרות אחרת";

            UnitOne[111, 0] = "בפרהסיה";
            UnitOne[111, 1] = "בגלוי, בפומבי";

            UnitOne[112, 0] = "ברי";
            UnitOne[112, 1] = "ברור, ודאי";

            UnitOne[113, 0] = "גדיל";
            UnitOne[113, 1] = "קווצה של חוטים בשולי הבד, ציצית";

            UnitOne[114, 0] = "גחון";
            UnitOne[114, 1] = "כפוף";

            UnitOne[115, 0] = "גמע";
            UnitOne[115, 1] = "לגם, שתה / האזין בעניין רב";

            UnitOne[116, 0] = "דאבה";
            UnitOne[116, 1] = "צער, יגון";

            UnitOne[117, 0] = "דוברה";
            UnitOne[117, 1] = "רפסודה";

            UnitOne[118, 0] = "דלוח";
            UnitOne[118, 1] = "עכור, לא צלול";

            UnitOne[119, 0] = "היבריח";
            UnitOne[119, 1] = "נעל";

            UnitOne[120, 0] = "החרמה";
            UnitOne[120, 1] = "לקיחה בכוח של דבר כלשהו מרשות האחר";

            UnitOne[121, 0] = "הכביד את ליבו";
            UnitOne[121, 1] = "הקשה את ליבו, התעקש, עמד על דעתו";

            UnitOne[122, 0] = "הלעיט";
            UnitOne[122, 1] = "נתן הרבה מאוד מזון ומשקה / מילא בשפע של פרטים ומידע ללא הבחנה";

            UnitOne[123, 0] = "הנץ";
            UnitOne[123, 1] = "הצמיח ניצנים, החל לפרוח";

            UnitOne[124, 0] = "הסכין";
            UnitOne[124, 1] = "התרגל, הסתגל";

            UnitOne[125, 0] = "הערים";
            UnitOne[125, 1] = "הטעה או הוליך שולל מישהו על ידי שימוש בתחבולות ותכסיסים";

            UnitOne[126, 0] = "הצר את צעדיו";
            UnitOne[126, 1] = "הפריע לו, שם בפניו מיכשולים";
       
            UnitOne[127, 0] = "השחיז";
            UnitOne[127, 1] = "ליטש ושייף כלי בכדי להפכו לחד";

            UnitOne[128, 0] = "הישתרר";
            UnitOne[128, 1] = "החל להתקיים";

            UnitOne[129, 0] = "התחבט";
            UnitOne[129, 1] = "התלבט";

            UnitOne[130, 0] = "התנשא";
            UnitOne[130, 1] = "נע כלפי מעלה, בלט לגובה / התנהג ביהירות ושחצנות";

            UnitOne[131, 0] = "התריס";
            UnitOne[131, 1] = "הביע מחאה, טען נגד מישהו";

            UnitOne[132, 0] = "זיגג";
            UnitOne[132, 1] = "התקין זגוגית, ציפה בציפוי שקוף";

            UnitOne[133, 0] = "זרזיף";
            UnitOne[133, 1] = "זרם, שטף, גשם דק";

            UnitOne[134, 0] = "חיזר על הפתחים";
            UnitOne[134, 1] = "הלך מבית לבית";

            UnitOne[135, 0] = "חמס";
            UnitOne[135, 1] = "שוד, גזל";

            UnitOne[136, 0] = "חרי אף";
            UnitOne[136, 1] = "כעס, קצף";

            UnitOne[137, 0] = "טופר";
            UnitOne[137, 1] = "קצות האצבעות של בעלי החיים ";

            UnitOne[138, 0] = "ידה";
            UnitOne[138, 1] = "זרק אבנים, השליך בכוח למרחק";

            UnitOne[139, 0] = "יצאה נפשו אל..";
            UnitOne[139, 1] = "התגעגע אל";

            UnitOne[140, 0] = "יתרה מזו";
            UnitOne[140, 1] = "לא רק זאת אלא גם";

            UnitOne[141, 0] = "כורם";
            UnitOne[141, 1] = "האדם העובד בכרם הענבים";

            UnitOne[142, 0] = "כמרקחה";
            UnitOne[142, 1] = "הומה, רוגש";

            UnitOne[143, 0] = "כקליפת השום";
            UnitOne[143, 1] = "משל לדבר נטול חשיבות";

            UnitOne[144, 0] = "ליבן";
            UnitOne[144, 1] = "חימם מתכת באש עד להלבנתה / דן בבעיה ובירר אותה מכל צדדיה";

            UnitOne[145, 0] = "למכביר";
            UnitOne[145, 1] = "בשפע, הרבה מאוד";

            UnitOne[146, 0] = "לרבות";
            UnitOne[146, 1] = "כולל, וגם, להוסיף";

            UnitOne[147, 0] = "מבושם";
            UnitOne[147, 1] = "מלא בושם / שיכור במקצת";

            UnitOne[148, 0] = "מדוה";
            UnitOne[148, 1] = "כאב, מחלה";

            UnitOne[149, 0] = "מהמורה";
            UnitOne[149, 1] = "בור, שוחה";

            //UnitOne[150, 0] = "מורשה";
            //UnitOne[150, 1] = "מיופה כוח";

            //UnitOne[151, 0] = "מך";
            //UnitOne[151, 1] = "עני דל / ענו, צנוע ";

            //UnitOne[152, 0] = "מליצה";
            //UnitOne[152, 1] = "ביטוי פיוטי שלא נוהגים להשתמש בו ביום יום";

            //UnitOne[153, 0] = "מסוכה";
            //UnitOne[153, 1] = "גדר חיה המורכבת מצמחים";

            //UnitOne[154, 0] = "מפח נפש";
            //UnitOne[154, 1] = "אכזבה קשה, ייאוש";

            //UnitOne[155, 0] = "מיקשה";
            //UnitOne[155, 1] = "שדה דלועים, עשוי מחתיכה אחת";

            //UnitOne[156, 0] = "משאת נפש";
            //UnitOne[156, 1] = "אידיאל, דבר שהאדם שואף אליו";

            //UnitOne[157, 0] = "משמים";
            //UnitOne[157, 1] = "משעמם";

            //UnitOne[158, 0] = "נאצה";
            //UnitOne[158, 1] = "חירוף, גידוף";

            //UnitOne[159, 0] = "נגע";
            //UnitOne[159, 1] = "פגע, צרה";

            //UnitOne[160, 0] = "נוגה";
            //UnitOne[160, 1] = "עצוב, עגום";

            //UnitOne[161, 0] = "נחוש";
            //UnitOne[161, 1] = "חזק וקשה";

            //UnitOne[162, 0] = "ניב";
            //UnitOne[162, 1] = "ביטוי / שן";

            //UnitOne[163, 0] = "נכמרו רחמיו";
            //UnitOne[163, 1] = "התעוררו בליבו רגשי רחמים";

            //UnitOne[164, 0] = "נסורת";
            //UnitOne[164, 1] = "שבבי עץ קטנים, אבק הניסור";

            //UnitOne[165, 0] = "נקף";
            //UnitOne[165, 1] = "הקיש, דפק. נאמר גם על זמן שחלף";

            //UnitOne[166, 0] = "ניתץ";
            //UnitOne[166, 1] = "שבר, ריסק";

            //UnitOne[167, 0] = "סואן";
            //UnitOne[167, 1] = "רועש, הומה";

            //UnitOne[168, 0] = "סכך";
            //UnitOne[168, 1] = "כיסה בגופו על משהו";

            //UnitOne[169, 0] = "סיקל";
            //UnitOne[169, 1] = "פינה את האזור מאבנים";

            //UnitOne[170, 0] = "עוון";
            //UnitOne[170, 1] = "חטא, עבירה";

            //UnitOne[171, 0] = "עילג";
            //UnitOne[171, 1] = "כבד פה, מגמגם";

            //UnitOne[172, 0] = "עלץ";
            //UnitOne[172, 1] = "שמח";

            //UnitOne[173, 0] = "";
            //UnitOne[173, 1] = "";

            //UnitOne[174, 0] = "";
            //UnitOne[174, 1] = "";

            //UnitOne[175, 0] = "";
            //UnitOne[175, 1] = "";

            //UnitOne[176, 0] = "";
            //UnitOne[176, 1] = "";

            //UnitOne[177, 0] = "";
            //UnitOne[177, 1] = "";

            //UnitOne[178, 0] = "";
            //UnitOne[178, 1] = "";

            //UnitOne[179, 0] = "";
            //UnitOne[179, 1] = "";

            question.AutoSize = false;
            question.Size = new System.Drawing.Size(100, 50);
            
        }
        private void Start_Click(object sender, EventArgs e) //כפתור התחלת המשחק
        {
            points = 0;
            poin.Text = points.ToString();
            life1.Text = "O";
            life2.Text = "O";
            life3.Text = "O";
            flag = true;
            num = y;
            button1.Show();
            button2.Show();
            button3.Show();
            button4.Show();
            questions.Clear();
            questions.Add(-1);
            NextQuestion();
        }
        private static bool QustionWas(List<int> L, int num)
        {
            for (int i = 0; i < L.Count; i++)
                if (L[i] == num)
                    return false;
            return true; ;
        }
        private void NextQuestion()
        {
            Delegate del = new DELEGATE(SubNextQustion);
            this.Invoke(del);

        }
        private void SubNextQustion() //פעולת השאלה הבאה
        {
            QuesTime.Enabled = true; //מתחיל את שעון השאלולת
            ticks = 6;
            question.Text = "";
            Start.Hide();
            this.label1.Text = " שאלה " + Counter.ToString(); //מציג באיזה שאלה אני

            randoms.Clear(); // היא הרשימה של המספרים המוגרלים randoms
            button1.Text = "";
            button2.Text = "";
            button3.Text = "";
            button4.Text = "";

            button1.BackColor = Color.Gainsboro;
            button2.BackColor = Color.Gainsboro;
            button3.BackColor = Color.Gainsboro;
            button4.BackColor = Color.Gainsboro;
            if ((!flag)&&(questions.Count() < num - x + 1))
            {
                questions.RemoveAt(questions.Count()-1);
                
            }
                Random rnd = new Random();
                it = rnd.Next(x, y); //it מגריל מספר לתוך 


            while ((questions.Count() <num - x +1) && (question.Text == ""))
                if (QustionWas(questions, it))
                {
                    questions.Add(it);
                    question.Text = UnitOne[it, 0]; // מכניס את המילה המוגרלת ללייבל של השאלה
                }
                else
                {
                    it = rnd.Next(x, y); //it מגריל מספר לתוך 
                }

            if (questions.Count() > y - x)
                question.Text = UnitOne[it, 0]; // מכניס את המילה המוגרלת ללייבל של השאלה


            buttons[rnd.Next(0, 4)].Text = UnitOne[it, 1]; //מגריל כפתור מממערך הכפתורים ומציג בו את הפירוש של השאלה
                                                           //בהתאם לאינדקס השמור

            randoms.Add(it); //הוספת המספר המוגרל לרשימה

            int j = 0;                 //whileמטרת ה
                                       //למלא את שאר הכפתורים הריקים בפירושים רנדומלים תוך ווידוא שלא תהיה תשובה פעמיים

            while (button1.Text == "" || button2.Text == "" || button3.Text == "" || button4.Text == "") //כל עוד אחד מהם ריק
            {
                if (buttons[j].Text == "")   // ניגש אל מערך הכפתורים ובודק האם ריק                       
                {
                    it = rnd.Next(x, y); //מגריל
                    if ((Exclude(randoms, it)) && (SpecifiesCeses(randoms, it))) //שולח את המספר המוגרל ואת רשימת המוגרלים אל הפעולה 
                    {                         //על מנת לבדוק שלא הוגרל כבר
                        randoms.Add(it);      // אם לא הוגרל עדיין, תוסיף לרשימה
                        buttons[j].Text = UnitOne[it, 1];//תכניס את הפירוש של ההגרלה לכפתור הריק
                        j++;                             //לך לכפתור הבא
                    }
                }
                else
                    j++;                     //במידה והכפתור היה כבר מלא, דלג לכפתור הבא 
            }

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }

        private static bool Exclude(List<int> L, int number)//מקבל מספר ורשימה
        {                                                   // מחזיר שקר אם המספר נמצא ברשימה, ואמת אם לא
                                                            // מטרת הפעולה לבדוק האם המספר כבר הוגרל 

            for (int i = 0; i < L.Count; i++)
                if (L[i] == number)
                    return false;
            return true; ;
        }
        private static bool SpecifiesCeses(List<int> L, int number)
        {
            if ((number == 53) && (L.Contains(58)))
                return false;
            if ((number == 58) && (L.Contains(53)))
                return false;
            return true;
        }

        private static int GetLocation(string[,] arr, string word)//פעולה המחזירה את מיקום המילה במערך 
        {                                                         //בטור הראשון נמצאים המילים, בטור השני הפירושים
            int i = 0;
            while ((arr[i, 0] != word) && arr.GetLength(0) >= i)
            {
                i++;
            }
            return i;
        }
        private static bool ChecksAnswer(Button btn, string[,] arr, string str)//הפעולה מקבלת את הכפתור הנלחץ, מערך המילים והפירושים ואת המילה בשאלה
                                                                               //מחזירה אמת אם התשובה נכונה ושקר אם לא
        {
            int location;                                                      //כך אני יודעת את האינדקס של הפירוש , GetLocation המערך והמילה נשלחים לפעולה
            location = GetLocation(arr, str);                                  // ואז אני משווה את הטקסט בכפתור לפירוש
            if (btn.Text == arr[location, 1])
            {
                return true;
            }
            return false;
        }
        private void Atime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Delegate del1 = new DELEGATE(SubTimeElpased);
            this.Invoke(del1);
        }
        private async void SubTimeElpased() //זאת הפונקציה שהתוכנית מגיעה אליה אחרי שהאינטרבל נגמר
        {
            atime.Stop();
            if (flag) //אם התשובה נכונה
            {
                NextQuestion();
            }
            else
            {
                B.BackColor = Color.Gainsboro;
                buttons[location].BackColor = Color.Green;

                await Task.Delay(1000); //מעכב את הבדיקות כדי שהתשובה הנכונה תישאר ירוקה 

                if (points <= 0) //אם הגעת למינוס נקודות הפסדת בלי קשר למספר החיים
                {
                    life1.Text = "X";
                    life2.Text = "X";
                    life3.Text = "X";
                    attempts = 0;
                    Counter = 1;
                    MessageBox.Show("Game Over. You are a loser");
                    Start.Show();
                }
                if (attempts == 3) // אם יש שלוש טעויות המשחק נגמר
                {
                    attempts = 0;
                    Counter = 1;
                    MessageBox.Show("Game Over, you won " + points.ToString() + " points");
                    Start.Show();
                }
                if (life3.Text == "O") //אם החיים האחרון עדיין לא נפסל תמשיך האלה
                {                      //"game over"נמצא כאן על מנת שלא תוצג השאלה הבאה לאחר הודעת ה
                    Counter++;
                    NextQuestion();
                }
            }
        }
        public void Wrong(Button btn) //מגיע לכאן כאשר נלחצת תשובה שגויה
                                      //מקבל את הכפתור השגוי שנלחץ
        {
            QuesTime.Enabled = false;  //עוצר את השעון ומאפס
            ticks = 6;

            attempts++;                     //סופר את מספר הכשלונות
            B = btn;
            B.BackColor = Color.Red;
            wrongAns.Play();
            atime.Start(); // atime_elpased טיימר מתחיל וכשמגיע ליעד נכנס לפעולה  

            if (life1.Text == "O")          //מוריד את החיים הראשון, השני ולאחר מכן השלישי
                life1.Text = "X";
            else if (life2.Text == "O")
                life2.Text = "X";
            else
                life3.Text = "X";

            location = GetLocation(UnitOne, question.Text); // מחפש את מיקום המילה שבשאלה במערך על מנת לדעת איפה האינדקס של הפירוש הנכון

            for (int i = 0; i < buttons.Length; i++) //רץ על מערך הכפתורים ומחפש היכן נמצא הפירוש הנכון
            {
                if (buttons[i].Text == UnitOne[location, 1])
                {
                    location = i; //שומר את מספר הכפתור הנכון
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false; //לאחר בחירת תשובה מנטרל את הכפתורים
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;


            flag = ChecksAnswer(button1, UnitOne, question.Text);//בודק האם התשובה נכונה
            if (flag) //אם כן
            {
                QuesTime.Enabled = false;         //הפסק את השעון
                atime.Start();                  //atime_elpased טיימר מתחיל וכשמגיע ליעד נכנס לפעולה 
                button1.BackColor = Color.Green;
                correct.Play();
                points = points + 10;
                poin.Text = points.ToString();
                Counter++;

            }
            else //אם תשובה שגויה, שלח לפעולה
            {
                points = points - 10; //מוריד 10 נקודות במקרה של תשובה שגויה
                poin.Text = points.ToString();
                Wrong(button1);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            flag = ChecksAnswer(button2, UnitOne, question.Text);
            if (flag)
            {
                QuesTime.Enabled = false;
                atime.Start();
                button2.BackColor = Color.Green;
                correct.Play();

                points = points + 10;
                poin.Text = points.ToString();
                Counter++;
            }
            else
            {
                points = points - 10;
                poin.Text = points.ToString();
                Wrong(button2);
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            flag = ChecksAnswer(button3, UnitOne, question.Text);
            if (flag)
            {
                QuesTime.Enabled = false;
                atime.Start();
                button3.BackColor = Color.Green;
                correct.Play();

                points = points + 10;
                poin.Text = points.ToString();
                Counter++;
            }
            else
            {
                points = points - 10;
                poin.Text = points.ToString();
                Wrong(button3);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            flag = ChecksAnswer(button4, UnitOne, question.Text);
            if (flag)
            {
                QuesTime.Enabled = false;
                atime.Start();
                button4.BackColor = Color.Green;
                correct.Play();
                points = points + 10;
                poin.Text = points.ToString();
                Counter++;
            }
            else
            {
                points = points - 10;
                poin.Text = points.ToString();
                Wrong(button4);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            x = int.Parse(textBox1.Text);
            y = int.Parse(textBox2.Text);
            if ((x < 0) || (y > 153))
                MessageBox.Show("טווח לא תקין");
            else
            {
                label7.Hide();
                label9.Hide();
                label10.Hide();
                textBox1.Hide();
                textBox2.Hide();
                button5.Hide();
                Start.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)//פעולה של שעון העצר
        {
            ticks = ticks - 1;
            label6.Text = ticks.ToString();

            if (ticks == 0) //כשנגמר הזמן שלח לפעולה
            {
                points = points - 10;
                poin.Text = points.ToString();
                flag = false;
                Wrong(timebtn);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}