namespace GoogleAnalyticsAPI.Models;

public class ExchangeUAH
{
    public string StartDate { get; set; }
    public long TimeSign { get; set; }
    public int CurrencyCode { get; set; }
    public string CurrencyCodeL { get; set; }
    public int Units { get; set; }
    public decimal Amount { get; set; }
}