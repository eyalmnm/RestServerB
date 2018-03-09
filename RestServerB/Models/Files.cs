using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    // Relacting an item in XXXX table
    public class Files
    {
        public long FilesId { set; get; }
        public string FileNumber { set; get; }
        public long ReportType { set; get; }
        public long ReportTypeSub { set; get; }
        public string ReportTypeMoreInfo { set; get; }
        public long ClerkId { set; get; }
        public long MainApprasierId { set; get; }
        public DateTime OrderDate { set; get; }
        public string IssueNumber { set; get; }
        public long OrderSource { set; get; }
        public long OrderName { set; get; }
        public string PolicyNumber { set; get; }
        public string CallNumber { set; get; }
        public long ContactId { set; get; }
        public long ThirdPartyId { set; get; }
        public long InsuredId { set; get; }
        public string ContactName { set; get; }
        public string ContactMobile { set; get; }
        public string SuitNumber { set; get; }
        public long AgentId { set; get; }
        public long AgentClerkId { set; get; }
        public string EventAddress { set; get; }
        public long EventCity { set; get; }
        public DateTime EventDate { set; get; }
        public DateTime EventDateTo { set; get; }
        public DateTime CheckDate { set; get; }
        public DateTime RecieveDocDate { set; get; }
        public DateTime SendReportDate { set; get; }
        public long FileStatus { set; get; }
        public long FileShape { set; get; }
        public long FileStorageType { set; get; }
        public string FileStorageInfo { set; get; }
        public long Debt { set; get; }
        public DateTime CreationDate { set; get; }
        public DateTime CloseDate { set; get; }
        public DateTime LastPaymentUpdate { set; get; }
        public bool LegalFile { set; get; }
        public long NumberOfFiles { set; get; }
        public string ContactPhoneWork { set; get; }
        public string ContactPhoneHome { set; get; }
        public string ContactEmail { set; get; }
        public long StructureSum { set; get; }
        public long ContentSum { set; get; }
        public long SelfParticipationSum { set; get; }
        public DateTime InsPeriodFrom { set; get; }
        public DateTime InsPeriodTo { set; get; }
        public long PolicyType { set; get; }
        public long PolicyName { set; get; }
        public string SeaCargo { set; get; }
        public string SeaMovilAviri { set; get; }
        public string SeaShip { set; get; }
        public DateTime SeaIsraelArrivalDate { set; get; }
        public long SeaArrivalPort { set; get; }
        public string SeaContainer { set; get; }
        public string SeaMezhar { set; get; }
        public string SeaCargoBillNum { set; get; }
        public string SeaInsideCargoBillNum { set; get; }
        public DateTime SeaFinalDestArrivalDate { set; get; }
        public string SeaDamage { set; get; }
        public string SeaAriza { set; get; }
        public string SeaTosefet { set; get; }
        public double SeaBillValue { set; get; }
        public long SeaBillCoin { set; get; }
        public double SeaInsSum { set; get; }
        public long SeaCustomsAgent { set; get; }
        public long Coin { set; get; }
        public DateTime UpdateDate { set; get; }
        public string FileRemark { set; get; }
        public long CreateReminders { set; get; }
        public long Contact2Id { set; get; }
        public long Contact3Id { set; get; }
        public double ElectronicSum { set; get; }
        public double SheverMechaniSum { set; get; }
        public double HarchavotSum { set; get; }
        public DateTime SendFirstMessageDate { set; get; }
        public long PolicyTosefetNum { set; get; }
        public DateTime PolicyUpdateDate { set; get; }
        public long ApartmentType { set; get; }
        public long CodeMadorType { set; get; }
        public string SuitNumberSub { set; get; }
        public double EstimatedDamageValue { set; get; }
        public DateTime SendTosefetDate { set; get; }
        public long PersonAdd1 { set; get; }
        public long PersonAdd2 { set; get; }
        public string BetMishpatNum { set; get; }
        public double TachshitimSum { set; get; }
        public DateTime SendBillDate { set; get; }
        public long Insured2Id { set; get; }
        public long Insured3Id { set; get; }
        public double EstimatedDamageStructure { set; get; }
        public double EstimatedDamageContent { set; get; }
        public double EstimatedDamageTahshitim { set; get; }
        public double EstimatedDamageMlay { set; get; }
        public double EstimatedDamageUvdanRevahim { set; get; }
        public double EstimatedDamageOther { set; get; }

        public Files(long fileId, string fileNumber, long reportType, long reportTypeSub, string reportTypeMoreInfo,
            long clerkId, long mainApprasierId, DateTime orderDate, string issueNumber, long orderSource, long orderName,
            string policyNumber, string callNumber, long contactId, long thirdPartyId, long insuredId, string contactName,
            string contactMobile, string suitNumber, long agentId, long agentClerkId, string eventAddress, long eventCity,
            DateTime eventDate, DateTime eventDateTo, DateTime checkDate, DateTime recieveDocDate, DateTime sendReportDate,
            long fileStatus, long fileShape, long fileStorageType, string fileStorageInfo, long debt, DateTime creationDate,
            DateTime closeDate, DateTime lastPaymentUpdate, bool legalFile, long numberOfFiles, string contactPhoneWork,
            string contactPhoneHome, string contactEmail, long structureSum, long contentSum, long selfParticipationSum,
            DateTime insPeriodFrom, DateTime insPeriodTo, long policyType, long policyName, string seaCargo,
            string seaMovilAviri, string seaShip, DateTime seaIsraelArrivalDate, long seaArrivalPort, string seaContainer,
            string seaMezhar, string seaCargoBillNum, string seaInsideCargoBillNum, DateTime seaFinalDestArrivalDate,
            string seaDamage, string seaAriza, string seaTosefet, double seaBillValue, long seaBillCoin, double seaInsSum,
            long seaCustomsAgent, long coin, DateTime updateDate, string fileRemark, long createReminders, long contact2Id,
            long contact3Id, double electronicSum, double sheverMechaniSum, double harchavotSum, DateTime sendFirstMessageDate,
            long policyTosefetNum, DateTime policyUpdateDate, long apartmentType, long codeMadorType, string suitNumberSub,
            double estimatedDamageValue, DateTime sendTosefetDate, long personAdd1, long personAdd2, string betMishpatNum,
            double tachshitimSum, DateTime sendBillDate, long insured2Id, long insured3Id, double estimatedDamageStructure,
            double estimatedDamageContent, double estimatedDamageTahshitim, double estimatedDamageMlay,
            double estimatedDamageUvdanRevahim, double estimatedDamageOther)
        {
            this.FilesId = fileId;
            this.FileNumber = fileNumber;
            this.ReportType = reportType;
            this.ReportTypeSub = reportTypeSub;
            this.ReportTypeMoreInfo = reportTypeMoreInfo;
            this.ClerkId = clerkId;
            this.MainApprasierId = mainApprasierId;
            this.OrderDate = orderDate;
            this.IssueNumber = issueNumber;
            this.OrderSource = orderSource;
            this.OrderName = orderName;
            this.PolicyNumber = policyNumber;
            this.CallNumber = callNumber;
            this.ContactId = contactId;
            this.ThirdPartyId = thirdPartyId;
            this.InsuredId = insuredId;
            this.ContactName = contactName;
            this.ContactMobile = contactMobile;
            this.SuitNumber = suitNumber;
            this.AgentId = agentId;
            this.AgentClerkId = agentClerkId;
            this.EventAddress = eventAddress;
            this.EventCity = eventCity;
            this.EventDate = eventDate;
            this.EventDateTo = eventDateTo;
            this.CheckDate = checkDate;
            this.RecieveDocDate = recieveDocDate;
            this.SendReportDate = sendReportDate;
            this.FileStatus = fileStatus;
            this.FileShape = fileShape;
            this.FileStorageType = fileStorageType;
            this.FileStorageInfo = fileStorageInfo;
            this.Debt = debt;
            this.CreationDate = creationDate;
            this.CloseDate = closeDate;
            this.LastPaymentUpdate = lastPaymentUpdate;
            this.LegalFile = legalFile;
            this.NumberOfFiles = numberOfFiles;
            this.ContactPhoneWork = contactPhoneWork;
            this.ContactPhoneHome = contactPhoneHome;
            this.ContactEmail = contactEmail;
            this.StructureSum = structureSum;
            this.ContentSum = contentSum;
            this.SelfParticipationSum = selfParticipationSum;
            this.InsPeriodFrom = insPeriodFrom;
            this.InsPeriodTo = insPeriodTo;
            this.PolicyType = policyType;
            this.PolicyName = policyName;
            this.SeaCargo = seaCargo;
            this.SeaMovilAviri = seaMovilAviri;
            this.SeaShip = seaShip;
            this.SeaIsraelArrivalDate = seaIsraelArrivalDate;
            this.SeaArrivalPort = seaArrivalPort;
            this.SeaContainer = seaContainer;
            this.SeaMezhar = seaMezhar;
            this.SeaCargoBillNum = seaCargoBillNum;
            this.SeaInsideCargoBillNum = seaInsideCargoBillNum;
            this.SeaFinalDestArrivalDate = seaFinalDestArrivalDate;
            this.SeaDamage = seaDamage;
            this.SeaAriza = seaAriza;
            this.SeaTosefet = seaTosefet;
            this.SeaBillValue = seaBillValue;
            this.SeaBillCoin = seaBillCoin;
            this.SeaInsSum = seaInsSum;
            this.SeaCustomsAgent = seaCustomsAgent;
            this.Coin = coin;
            this.UpdateDate = updateDate;
            this.FileRemark = fileRemark;
            this.CreateReminders = createReminders;
            this.Contact2Id = contact2Id;
            this.Contact3Id = contact3Id;
            this.ElectronicSum = electronicSum;
            this.SheverMechaniSum = sheverMechaniSum;
            this.HarchavotSum = harchavotSum;
            this.SendFirstMessageDate = sendFirstMessageDate;
            this.PolicyTosefetNum = policyTosefetNum;
            this.PolicyUpdateDate = policyUpdateDate;
            this.ApartmentType = apartmentType;
            this.CodeMadorType = codeMadorType;
            this.SuitNumberSub = suitNumberSub;
            this.EstimatedDamageValue = estimatedDamageValue;
            this.SendTosefetDate = sendTosefetDate;
            this.PersonAdd1 = personAdd1;
            this.PersonAdd2 = personAdd2;
            this.BetMishpatNum = betMishpatNum;
            this.TachshitimSum = tachshitimSum;
            this.SendBillDate = sendBillDate;
            this.Insured2Id = insured2Id;
            this.Insured3Id = insured3Id;
            this.EstimatedDamageStructure = estimatedDamageStructure;
            this.EstimatedDamageContent = estimatedDamageContent;
            this.EstimatedDamageTahshitim = estimatedDamageTahshitim;
            this.EstimatedDamageMlay = estimatedDamageMlay;
            this.EstimatedDamageUvdanRevahim = estimatedDamageUvdanRevahim;
            this.EstimatedDamageOther = estimatedDamageOther;
        }
    }
}