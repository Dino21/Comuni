using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CM18tool.Arca;
using static CM18tool.CmDLLs;
using static myArcaNameSpace.Module;

public class CM50
{
    public string serialNumber { get; set; }
    public LAN lan { get; set; }
    public Product product { get; set; }
    public Client client { get; set; }
    public DD dd { get; set; }
    public Asset asset { get; set; }
    public CDF cdf { get; set; }
    public int ecoModeTimeout { get; set; }
    public Slot slots { get; set; }
    public SBanks[] banks { get; set; }
    public UnitConfiguration UnitConfiguration { get; set; }
    public CmConfig CmConfig { get; set; }
}
//public class UnitConfiguration
//{
//    public int idUnitConfiguration { get; set; }
//    public string cfg { get; set; }
//    public string optCfg { get; set; }
//    public string optOneCfg { get; set; }
//    public string optTwoCfg { get; set; }
//    public string optThreeCfg { get; set; }
//}
public class Slot
{
    public int activeNumber { get; set; }
    public string currency { get; set; }
    public SBanks[] banks { get; set; }
}
public class LAN
{
    public string IP {  get; set; }
    public string Port { get; set; }
    public string DHCP { get; set; }
    public string MAC { get; set; }
    public string subnet    { get; set; }
    public string name { get; set; }
    public string primaryDNS { get; set; }
    public string secondaryDNS { get; set; }
    public string useNetBios { get; set; }
    public string description { get; set; }
    public string gateway { get; set; }

}
public class Client
{
    public int idClient { get; set; }
    public string clientName { get; set; }
}

public class Configuration
{
    public int idProduct {  get; set; }
    public string productCode { get; set; }
    public int idClient { get; set; }
    public string DDtransitionCode { get; set; }
    public string DDcode { get; set; }
    public string ASSETcode { get; set; }
    public string CDFcode { get; set; }
    public int idUnitConfiguration { get; set; }
    public int idReaderCfg { get; set; }
    public int idCassetteCfg { get; set; }
    public string alarmType { get; set; }
}

public class Asset
{
    public int idAsset { get; set; }
    public string code { get; set; }
    public string internalName { get; set; }
    public string shortFilename { get; set; }
    public string tallFilename { get; set; }
    public string version { get; set; }
    public string release {  get; set; }
    public string serial { get; set; }
}

public class CDF
{
    public int idCdf { get; set; }
    public string code { get; set; }
    public string internalName { get; set; }
    public List<string> currCode { get; set; }
}

public class DD
{
    public int idDD { get; set; }
    public string code { get; set; }
    public string internalName { get; set; }
    public string transitionDDCode { get; set; }
    public string transitionFilename { get; set; }
    public string aSISver { get; set; }
}


public class Product
{
    public int idProductCode { get; set; }
    public string code { get; set; }
    public string description { get; set; }
    public short cassetteNumber { get; set; }
    public string machineType { get; set; }
    public string cassetteName { get; set; }
    public string upperLock { get; set; }
    public string lowerLock { get; set; }
    public string readingStation { get; set; }
    public string barcodeReader { get; set; }
    public string alarmType { get; set; }
}



public class CmConfig
{
    public string hexValue { get; set; }
    public string CmDescription { get; set; }
    public string CmBDescription { get; set; }
    public string NvDescription { get; set; }
}

public class UserCustomization
{
    public int idUserCustomization { get; set; }
    // public int idProduct { get; set; }
    public Product product { get; set; }
    //public int idClient { get; set; }
    public Client client { get; set; }
    public UnitConfiguration unitConfiguration { get; set; }
    public DD dd { get; set; }
    public Asset asset { get; set; }
    public CDF cdf { get; set; }
    public int activeBanks { get; set; }
    public string currency { get; set; }
    public string bankConfig { get; set; }
    public List<Banks> bank { get; set; }
    public int ecoModeTimeout { get; set; }
    public string setCassConfig { get; set; }
    public string countryCode { get; set; }
    public string displayLanguage { get; set; }
    public int systemRate { get; set; }
}

public class Banks
{
    public string reference;
    public string fitness;
}