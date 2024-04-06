using InfluencerManagerApp.Core.Contracts;
using InfluencerManagerApp.Models;
using InfluencerManagerApp.Models.Contracts;
using InfluencerManagerApp.Repositories;
using InfluencerManagerApp.Repositories.Contracts;
using InfluencerManagerApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfluencerManagerApp.Core
{
    public class Controller : IController
    {
        private IRepository<IInfluencer> influencers;
        private IRepository<ICampaign> campaigns;

        public Controller()
        {
            influencers = new InfluencerRepository();
            campaigns = new CampaignRepository();
        }

        public string ApplicationReport()
        {
            StringBuilder sb = new StringBuilder();
            var orderedInfluencers = influencers.Models.OrderByDescending(i => i.Income).ThenByDescending(i => i.Followers);

            foreach (var influencer in orderedInfluencers)
            {
                sb.AppendLine(influencer.ToString());
                
                if (influencer.Participations.Any())
                {
                    sb.AppendLine("Active Campaigns:");
                    foreach (var participation in influencer.Participations.OrderBy(p => p)) 
                    {
                        ICampaign campaign = campaigns.FindByName(participation);
                        sb.AppendLine($"--{campaign.ToString()}");
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }

        public string AttractInfluencer(string brand, string username)
        {
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotFound, influencers.GetType().Name, username);
            }
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.CampaignNotFound, brand);
            }

            if (campaign.Contributors.Contains(username))
            {
                return string.Format(OutputMessages.InfluencerAlreadyEngaged, username, brand);
            }

            if (campaign.GetType() == typeof(ProductCampaign))
            {
                if (influencer.GetType() != typeof(BusinessInfluencer) && influencer.GetType() != typeof(FashionInfluencer))
                {
                    return string.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
                }
            }
            else if (campaign.GetType() == typeof(ServiceCampaign))
            {
                if (influencer.GetType() != typeof(BusinessInfluencer) && influencer.GetType() != typeof(BloggerInfluencer))
                {
                    return string.Format(OutputMessages.InfluencerNotEligibleForCampaign, username, brand);
                }
            }

            int campaignPrice = influencer.CalculateCampaignPrice();
            if (campaign.Budget < campaignPrice)
            {
                return string.Format(OutputMessages.UnsufficientBudget, brand, username);
            }

            influencer.EarnFee(campaignPrice);
            influencer.EnrollCampaign(brand);
            campaign.Engage(influencer);

            return string.Format(OutputMessages.InfluencerAttractedSuccessfully, username, brand);
        }

        public string BeginCampaign(string typeName, string brand)
        {
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign != null)
            {
                return string.Format(OutputMessages.CampaignDuplicated, brand);
            }
            else
            {
                if (typeName == nameof(ProductCampaign))
                {
                    campaign = new ProductCampaign(brand);
                }
                else if (typeName == nameof(ServiceCampaign))
                {
                    campaign = new ServiceCampaign(brand);
                }
                else
                {
                    return string.Format(OutputMessages.CampaignTypeIsNotValid, typeName);
                }

                campaigns.AddModel(campaign);
                return string.Format(OutputMessages.CampaignStartedSuccessfully, brand, typeName);
            }
        }

        public string CloseCampaign(string brand)
        {
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.InvalidCampaignToClose);
            }
            if (campaign.Budget <= 10000)
            {
                return string.Format(OutputMessages.CampaignCannotBeClosed, brand);
            }

            foreach (var contributor in campaign.Contributors)
            {
                var influencer = influencers.FindByName(contributor);
                influencer.EarnFee(2000);
                influencer.EndParticipation(brand);
            }

            campaigns.RemoveModel(campaign);
            return string.Format(OutputMessages.CampaignClosedSuccessfully, brand);
        }

        public string ConcludeAppContract(string username)
        {
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer == null)
            {
                return string.Format(OutputMessages.InfluencerNotSigned, username);
            }
            if (influencer.Participations.Any())
            {
                return string.Format(OutputMessages.InfluencerHasActiveParticipations, username);
            }

            influencers.RemoveModel(influencer);
            return string.Format(OutputMessages.ContractConcludedSuccessfully, username);
        }

        public string FundCampaign(string brand, double amount)
        {
            ICampaign campaign = campaigns.FindByName(brand);
            if (campaign == null)
            {
                return string.Format(OutputMessages.InvalidCampaignToFund);
            }
            if (amount <= 0)
            {
                return string.Format(OutputMessages.NotPositiveFundingAmount);
            }

            campaign.Gain(amount);
            return string.Format(OutputMessages.CampaignFundedSuccessfully, brand, amount);
        }

        public string RegisterInfluencer(string typeName, string username, int followers)
        {
            IInfluencer influencer = influencers.FindByName(username);
            if (influencer != null)
            {
                return string.Format(OutputMessages.UsernameIsRegistered, username, influencers.GetType().Name);
            }
            else
            {
                if (typeName == nameof(BloggerInfluencer))
                {
                    influencer = new BloggerInfluencer(username, followers);
                }
                else if (typeName == nameof(BusinessInfluencer))
                {
                    influencer = new BusinessInfluencer(username, followers);
                }
                else if (typeName == nameof(FashionInfluencer))
                {
                    influencer = new FashionInfluencer(username, followers);
                }
                else
                {
                    return string.Format(OutputMessages.InfluencerInvalidType, typeName);
                }

                influencers.AddModel(influencer);
                return string.Format(OutputMessages.InfluencerRegisteredSuccessfully, username);
            }
        }
    }
}
