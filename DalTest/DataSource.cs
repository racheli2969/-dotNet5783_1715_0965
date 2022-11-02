using DO;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        S_Initalize(0);
    }
   private static void S_Initalize(int type)
    {
        switch (type)
        {
            case 0:Add_Items(); break;
                default:Add_Items(); break;
        }
    }

    /// <summary>
    /// adds items to the item array
    /// </summary>
    private static void Add_Items()
    {

    }
    public static readonly Random Number =
       new Random();
    internal static Item[] Items;

    internal class Config
    {
        /// <summary>
        /// indexs for the first available place in the data arrays
        /// </summary>
        internal int CountItems = 0;
    }
}

