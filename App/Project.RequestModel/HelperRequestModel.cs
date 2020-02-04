namespace Project.RequestModel
{
    public static class HelperRequestModel
    {
        public static string GetCustomerCode(string right)
        {
            var len = right.Length;

            for (var i = 0; i < 5 - len; i++)
            {
                right = string.Concat("0", right);
            }
            var code = string.Concat("", right);

            return code;
        }

        public static string GetCaseNo(string right)
        {
            var len = right.Length;

            for (var i = 0; i < 5 - len; i++)
            {
                right = string.Concat("0", right);
            }
            var code = string.Concat("", right);

            return code;
        }



        public static string GenerateBillCode(string patientCode, string right)
        {
            int len = right.Length;

            for (int i = 0; i < 3 - len; i++)
            {
                right = string.Concat("0", right);
            }

            string billId = string.Concat("BILL", right);

            return string.Concat(billId + "-", patientCode);
        }



        public static string GetPatientCode(string right)
        {
            int len = right.Length;

            for (int i = 0; i < 6 - len; i++)
            {
                right = string.Concat("0", right);
            }
            string code = string.Concat("P", right);

            return code;
        }

        public static string GenerateAppointmentCode(string right)
        {
            int len = right.Length;

            for (int i = 0; i < 3 - len; i++)
            {
                right = string.Concat("0", right);
            }
            string code = string.Concat("AP", right);

            return code;
        }


        public static string GeneratePrescriptionCode(string patientCode, string right)
        {
            int len = right.Length;

            for (int i = 0; i < 3 - len; i++)
            {
                right = string.Concat("0", right);
            }

            string prescriptionId = string.Concat("PRES", right);

            return string.Concat(prescriptionId + "-", patientCode);
        }

       
    }
}
