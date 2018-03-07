using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestServerB.Models
{
    // Relacting an item in XXXX table
    public class Files
    {
        public long FilesId { get; }
        public string FileNumber { get; }
        public long ReportType { get; }
        public long ReportTypeSub { get; }
        public string ReportTypeMoreInfo { get; }
        public long ClerkId { get; }
        public long MainApprasierId { get; }
        public DateTime OrderDate { get; }
        public string IssueNumber { get; }
        public long OrderSource { get; }
        public long OrderName { get; }
        public string PolicyNumber { get; }
        public string CallNumber { get; }
        public long ContactId { get; }
        public long ThirdPartyId { get; }
        public long InsuredId { get; }
        public string ContactName { get; }
        public string ContactMobile { get; }
        public string SuitNumber { get; }
        public long AgentId { get; }
        public long AgentClerkId { get; }
        public string EventAddress { get; }
        public long EventCity { get; }
        public DateTime EventDate { get; }
        public DateTime EventDateTo { get; }
        public DateTime CheckDate { get; }
        public DateTime RecieveDocDate { get; }
        public DateTime SendReportDate { get; }
        public long FileStatus { get; }
        public long FileShape { get; }
        public long FileStorageType { get; }
        public string FileStorageInfo { get; }
        public long Debt { get; }
        public DateTime CreationDate { get; }
        public DateTime CloseDate { get; }
        public DateTime LastPaymentUpdate { get; }
        public bool LegalFile { get; }
        public long NumberOfFiles { get; }
        public string ContactPhoneWork { get; }
        public string ContactPhoneHome { get; }
        public string ContactEmail { get; }
        public long StructureSum { get; }
        public long ContentSum { get; }
        public long SelfParticipationSum { get; }
        public DateTime InsPeriodFrom { get; }
        public DateTime InsPeriodTo { get; }
        public long PolicyType { get; }
        public long PolicyName { get; }
        public string SeaCargo { get; }
        public string SeaMovilAviri { get; }
        public string SeaShip { get; }
        public DateTime SeaIsraelArrivalDate { get; }
        public long SeaArrivalPort { get; }
        public string SeaContainer { get; }
        public string SeaMezhar { get; }
        public string SeaCargoBillNum { get; }
        public string SeaInsideCargoBillNum { get; }
        public DateTime SeaFinalDestArrivalDate { get; }
        public string SeaDamage { get; }
        public string SeaAriza { get; }
        public string SeaTosefet { get; }
        public double SeaBillValue { get; }
        public long SeaBillCoin { get; }
        public double SeaInsSum { get; }
        public long SeaCustomsAgent { get; }
        public long Coin { get; }
        public DateTime UpdateDate { get; }
        public string FileRemark { get; }
        public long CreateReminders { get; }
        public long Contact2Id { get; }
        public long Contact3Id { get; }
        public double ElectronicSum { get; }
        public double SheverMechaniSum { get; }
        public double HarchavotSum { get; }
        public DateTime SendFirstMessageDate { get; }
        public long PolicyTosefetNum { get; }
        public DateTime PolicyUpdateDate { get; }
        public long ApartmentType { get; }
        public long CodeMadorType { get; }
        public string SuitNumberSub { get; }
        public double EstimatedDamageValue { get; }
        public DateTime SendTosefetDate { get; }
        public long PersonAdd1 { get; }
        public long PersonAdd2 { get; }
        public string BetMishpatNum { get; }
        public double TachshitimSum { get; }
        public DateTime SendBillDate { get; }
        public long Insured2Id { get; }
        public long Insured3Id { get; }
        public double EstimatedDamageStructure { get; }
        public double EstimatedDamageContent { get; }
        public double EstimatedDamageTahshitim { get; }
        public double EstimatedDamageMlay { get; }
        public double EstimatedDamageUvdanRevahim { get; }
        public double EstimatedDamageOther { get; }

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