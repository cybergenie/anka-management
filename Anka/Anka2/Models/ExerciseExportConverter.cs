using Anka2.Services;
using System.Data;

namespace Anka2.Models
{
    public class ExerciseExportConverter
    {
        public static void ExerciseValueConvertor(ref DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                DataUitls.GenderConvertor(dr, "性别");
                DataUitls.DateConvertor(dr, "记录编号");
                BedUpConvertor(dr, "床上负荷");
                InRoomConvertor(dr, "室内负荷");
                OutRoomConvertor(dr, "室外负荷");
                OutSideConvertor(dr, "院外负荷");
            }
        }

        private static void BedUpConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            for (int i = 0; i < 4; i++)
            {
                if (BedUps[i] == "1")
                    result = "合格";
            }
            dr[columnName] = result;
        }

        private static void InRoomConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            for (int i = 4; i < 6; i++)
            {
                if (BedUps[i] == "1")
                    result = "合格";
            }
            dr[columnName] = result;
        }

        private static void OutRoomConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            for (int i = 6; i < 8; i++)
            {
                if (BedUps[i] == "1")
                    result = "合格";
            }
            dr[columnName] = result;
        }

        private static void OutSideConvertor(DataRow dr, string columnName)
        {
            string BedUp = dr[columnName] as string;
            string result = "不合格";

            string[] BedUps = BedUp.Split('|');
            if (BedUps[9] == "1")
                result = "合格";

            dr[columnName] = result;
        }

    }
}
