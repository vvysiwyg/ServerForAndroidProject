using System;

namespace ServerForAndroidProject.data
{
    public class Pharmacy
    {
        public Guid id { get; set; }
        public int number { get; set; }
        public string address { get; set; }
        public string workingWeekDay { get; set; }
        public string workingStartTime { get; set; }
        public string workingEndTime { get; set; }
        public double rating { get; set; }
        public bool isMedicineDeliver { get; set; }
        public string paymentOption { get; set; }
        public Guid pnId { get; set; }

        public Pharmacy(
            Guid id,
            int number,
            string address,
            string workingWeekDay,
            string workingStartTime,
            string workingEndTime,
            double rating,
            bool isMedicineDeliver,
            string paymentOption,
            Guid pnId)
        {
            this.id = id;
            this.number = number;
            this.address = address;
            this.workingWeekDay = workingWeekDay;
            this.workingStartTime = workingStartTime;
            this.workingEndTime = workingEndTime;
            this.rating = rating;
            this.isMedicineDeliver = isMedicineDeliver;
            this.paymentOption = paymentOption;
            this.pnId = pnId;
        }
    }
}
