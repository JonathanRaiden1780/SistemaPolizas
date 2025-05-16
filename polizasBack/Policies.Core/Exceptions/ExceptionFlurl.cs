using Flurl.Http;
namespace Policies.Core.Exceptions
{
    public static class ExceptionFlurl
    {
        public static void GlobalTryCatch(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is FlurlHttpException fex)
                {
                    throw new BusinessException(fex.GetResponseStringAsync().Result);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
