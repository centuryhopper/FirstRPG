using System;
using UnityEngine;

namespace DefectDataa
{
    // Class method for storing the list of defect data for
    // each supervisor, value stream, and boat model.

    /*
    Example of what the JSON output should look like for a single DefectData object:
        {
            SuperVisorId : 1
            ValueStreamId : 2
            BoatModelId : 3
            PartId : null
            DefectAreaId : null
            IsRemold : null
            HullNumber : null
            DefectDate : <currentTimeinMillis>
        }
    */
    [Serializable]
    public class DefectData
    {
        public int SuperVisorId;
        public int ValueStreamId;
        public int BoatModelId;
        public int PartId;
        public int DefectAreaId;
        public bool IsRemold;
        public int HullNumber;
        public DateTime DefectDate;

        public DefectData()
        {
            SuperVisorId = 1;
            ValueStreamId = 2;
            BoatModelId = 3;
            PartId = 4;
            DefectAreaId = 5;
            IsRemold = false;
            HullNumber = 6;
            DefectDate = DateTime.Now;
        }

        public DefectData(int SuperVisorId,
            int ValueStreamId,
            int BoatModelId,
            int PartId,
            int DefectAreaId,
            bool IsRemold,
            int HullNumber,
            DateTime DefectDate)
        {
            this.SuperVisorId = SuperVisorId;
            this.ValueStreamId = ValueStreamId;
            this.BoatModelId = BoatModelId;
            this.PartId = PartId;
            this.DefectAreaId = DefectAreaId;
            this.IsRemold = IsRemold;
            this.HullNumber = HullNumber;
            this.DefectDate = DefectDate;
        }
    }


}