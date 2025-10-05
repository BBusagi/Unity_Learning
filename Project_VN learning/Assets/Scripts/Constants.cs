/// <summary>
/// 常量合集
/// </summary>
public class Constants
{
    public static float DEFAULT_WAITING_SECONDS = 1.5f;

    //打字机效果单字等待时间
    public static float DEFAULT_TYPING_SPEED = 0.05f;
    public static float FAST_TYPING_SPEED = 0.01f;

    public static int DURATION_TIME = 1;    //动画持续时间
    public static int DEFAULT_START_LINE = 1;

    public static string STORY_PATH = "Assets/Resources/story/";
    public static string DEFAULT_STORY_FILE_NAME = "1";
    public static string EXCEL_FILE_EXTENSION = ".xlsx";

    public static string BACKGROUND_PATH = "image/background/";
    public static string AVATAR_PATH = "image/avatar/";
    public static string CHARACTER_PATH = "image/character/";
    public static string BUTTON_PATH = "image/icon/";
    public static string MUSIC_PATH = "audio/music/";
    public static string VOCAL_PATH = "audio/vocal/";

    public static string charaterActionAppearAt = "appearAt";
    public static string charaterActionMoveTo = "moveTo";
    public static string charaterActionDisappear = "disappear";
    public static string STORYCONTROL_End = "END";
    public static string STORYCONTROL_CHOICE = "CHOICE";

    //TODO: 将icon的配置单独设置
    public static string AUTO_ON = "play_1b";
    public static string AUTO_OFF = "play_1";
    public static float AUTO_WAITING_SECONDS = 1.5f;

    public static string SKIP_ON = "arrow_black";
    public static string SKIP_OFF = "arrow_2";
    public static float SKIP_WAITING_SECONDS = 0.02f;

    // saveload panel
    public static int DEFAULT_START_INDEX = 0;
    public static int SLOTS_PER_PAGE = 8;
    public static int TOTAL_SLOTS = 40;
    public static string COLON = ": ";
    public static string SAVE_GAME = "save_game";
    public static string LOAD_GAME = "load_game";
    public static string EMPTY_SLOT = "empty_game";


}
