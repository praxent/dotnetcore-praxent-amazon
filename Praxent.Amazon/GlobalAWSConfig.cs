using Amazon;

namespace Praxent.Amazon
{
    public static class GlobalAWSConfig
    {
        public static void SetAWSRegion(string region)
        {
            if (region?.Trim().Length > 0)
            {
                AWSConfigs.AWSRegion = region;
            }
        }
    }
}