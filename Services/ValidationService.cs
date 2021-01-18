namespace TaxServices
{
    public class ValidationService
    {
        public bool ValidateResponse(string response)
        {
            if ((response.StartsWith("{") && response.EndsWith("}")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
