public interface IFinancialInstrument
{
    string Symbol { get; }
    decimal CurrentPrice { get; }
    InstrumentType Type { get; }
}

public enum InstrumentType { Stock, Bond, Option, Future }

// 1. Generic portfolio
public class Portfolio<T> where T : IFinancialInstrument
{
    private Dictionary<T, int> _holdings = new(); // Instrument -> Quantity
    
    // TODO: Buy instrument
    public void Buy(T instrument, int quantity, decimal price)
    {
        // Validate: quantity > 0, price > 0
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than 0");
        if (price <= 0)
            throw new ArgumentException("Price must be greater than 0");
        
        // Add to holdings or update quantity
        if (_holdings.ContainsKey(instrument))
            _holdings[instrument] += quantity;
        else
            _holdings[instrument] = quantity;
    }
    
    // TODO: Sell instrument
    public decimal? Sell(T instrument, int quantity, decimal currentPrice)
    {
        // Validate: enough quantity
        if (!_holdings.ContainsKey(instrument) || _holdings[instrument] < quantity)
            throw new InvalidOperationException("Insufficient quantity to sell");
        
        // Remove/update holdings
        _holdings[instrument] -= quantity;
        if (_holdings[instrument] == 0)
            _holdings.Remove(instrument);
        
        // Return proceeds (quantity * currentPrice)
        return quantity * currentPrice;
    }
    
    // TODO: Calculate total value
    public decimal CalculateTotalValue()
    {
        // Sum of (quantity * currentPrice) for all holdings
        decimal total = 0;
        foreach (var kvp in _holdings)
        {
            total += kvp.Key.CurrentPrice * kvp.Value;
        }
        return total;
    }
    
    // TODO: Get top performing instrument
    public (T instrument, decimal returnPercentage)? GetTopPerformer(
        Dictionary<T, decimal> purchasePrices)
    {
        // Calculate return percentage for each
        // Return highest performer
        if (_holdings.Count == 0)
            return null;
        
        (T instrument, decimal returnPercentage) topPerformer = default;
        decimal maxReturn = decimal.MinValue;
        
        foreach (var holding in _holdings)
        {
            if (purchasePrices.ContainsKey(holding.Key))
            {
                decimal purchasePrice = purchasePrices[holding.Key];
                decimal returnPercentage = ((holding.Key.CurrentPrice - purchasePrice) / purchasePrice) * 100;
                
                if (returnPercentage > maxReturn)
                {
                    maxReturn = returnPercentage;
                    topPerformer = (holding.Key, returnPercentage);
                }
            }
        }
        
        return topPerformer;
    }
    
    public Dictionary<T, int> GetHoldings()
    {
        return new Dictionary<T, int>(_holdings);
    }
}

// 2. Specialized instruments
public class Stock : IFinancialInstrument
{
    public string Symbol { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public InstrumentType Type => InstrumentType.Stock;
    public string CompanyName { get; set; } = string.Empty;
    public decimal DividendYield { get; set; }
}

public class Bond : IFinancialInstrument
{
    public string Symbol { get; set; } = string.Empty;
    public decimal CurrentPrice { get; set; }
    public InstrumentType Type => InstrumentType.Bond;
    public DateTime MaturityDate { get; set; }
    public decimal CouponRate { get; set; }
}

// 3. Generic trading strategy
public class TradingStrategy<T> where T : IFinancialInstrument
{
    // TODO: Execute strategy on portfolio
    public void Execute(Portfolio<T> portfolio, 
        Func<T, bool> buyCondition,
        Func<T, bool> sellCondition)
    {
        // For each instrument in market data
        // Apply buy/sell conditions
        // Execute trades
        var holdings = portfolio.GetHoldings();
        
        // Check sell conditions for current holdings
        foreach (var holding in holdings.Keys.ToList())
        {
            if (sellCondition(holding))
            {
                try
                {
                    portfolio.Sell(holding, holdings[holding], holding.CurrentPrice);
                }
                catch { }
            }
        }
    }
    
    // TODO: Calculate risk metrics
    public Dictionary<string, decimal> CalculateRiskMetrics(IEnumerable<T> instruments)
    {
        // Return: Volatility, Beta, Sharpe Ratio
        var metrics = new Dictionary<string, decimal>();
        
        var instrumentList = instruments.ToList();
        if (instrumentList.Count == 0)
            return metrics;
        
        // Calculate average price
        decimal avgPrice = instrumentList.Average(i => i.CurrentPrice);
        
        // Calculate volatility (standard deviation)
        decimal variance = instrumentList.Average(i => 
            (i.CurrentPrice - avgPrice) * (i.CurrentPrice - avgPrice));
        decimal volatility = (decimal)Math.Sqrt((double)variance);
        
        // Simplified metrics
        metrics["Volatility"] = volatility;
        metrics["Beta"] = 1.0m; // Market beta
        metrics["SharpeRatio"] = avgPrice > 0 ? volatility / avgPrice : 0;
        
        return metrics;
    }
}

// 4. Price history with generics
public class PriceHistory<T> where T : IFinancialInstrument
{
    private Dictionary<T, List<(DateTime, decimal)>> _history = new();
    
    // TODO: Add price point
    public void AddPrice(T instrument, DateTime timestamp, decimal price)
    {
        // Add to history
        if (!_history.ContainsKey(instrument))
            _history[instrument] = new List<(DateTime, decimal)>();
        
        _history[instrument].Add((timestamp, price));
    }
    
    // TODO: Get moving average
    public decimal? GetMovingAverage(T instrument, int days)
    {
        // Calculate simple moving average
        if (!_history.ContainsKey(instrument) || _history[instrument].Count == 0)
            return null;
        
        var recentPrices = _history[instrument]
            .OrderByDescending(p => p.Item1)
            .Take(days)
            .Select(p => p.Item2)
            .ToList();
        
        if (recentPrices.Count == 0)
            return null;
        
        return recentPrices.Average();
    }
    
    // TODO: Detect trends
    public Trend DetectTrend(T instrument, int period)
    {
        // Return: Upward, Downward, Sideways
        if (!_history.ContainsKey(instrument) || _history[instrument].Count < period)
            return Trend.Sideways;
        
        var recentPrices = _history[instrument]
            .OrderBy(p => p.Item1)
            .TakeLast(period)
            .Select(p => p.Item2)
            .ToList();
        
        decimal firstPrice = recentPrices.First();
        decimal lastPrice = recentPrices.Last();
        decimal changePercentage = ((lastPrice - firstPrice) / firstPrice) * 100;
        
        if (changePercentage > 2)
            return Trend.Upward;
        else if (changePercentage < -2)
            return Trend.Downward;
        else
            return Trend.Sideways;
    }
    
    public List<(DateTime, decimal)> GetHistory(T instrument)
    {
        if (_history.ContainsKey(instrument))
            return new List<(DateTime, decimal)>(_history[instrument]);
        return new List<(DateTime, decimal)>();
    }
}

public enum Trend { Upward, Downward, Sideways }

// 5. TEST SCENARIO: Trading simulation
// a) Create portfolio with mixed instruments
// b) Implement buy/sell logic
// c) Create trading strategy with lambda conditions
// d) Track price history
// e) Demonstrate:
//    - Portfolio rebalancing
//    - Risk calculation
//    - Trend detection
//    - Performance comparison

class Program
{
    static void Main()
    {
        Console.WriteLine("========== FINANCIAL TRADING PLATFORM ==========\n");
        
        // a) Create portfolio with mixed instruments
        Console.WriteLine("--- Creating Instruments ---");
        
        var apple = new Stock
        {
            Symbol = "AAPL",
            CurrentPrice = 150m,
            CompanyName = "Apple Inc.",
            DividendYield = 0.5m
        };
        
        var microsoft = new Stock
        {
            Symbol = "MSFT",
            CurrentPrice = 310m,
            CompanyName = "Microsoft Corporation",
            DividendYield = 0.8m
        };
        
        var governmentBond = new Bond
        {
            Symbol = "US10Y",
            CurrentPrice = 98.5m,
            MaturityDate = new DateTime(2030, 12, 31),
            CouponRate = 4.5m
        };
        
        var corporateBond = new Bond
        {
            Symbol = "CORP2030",
            CurrentPrice = 95.0m,
            MaturityDate = new DateTime(2030, 6, 30),
            CouponRate = 5.0m
        };
        
        Console.WriteLine($"Created: {apple.Symbol} - ${apple.CurrentPrice} (Dividend: {apple.DividendYield}%)");
        Console.WriteLine($"Created: {microsoft.Symbol} - ${microsoft.CurrentPrice} (Dividend: {microsoft.DividendYield}%)");
        Console.WriteLine($"Created: {governmentBond.Symbol} - ${governmentBond.CurrentPrice} (Coupon: {governmentBond.CouponRate}%)");
        Console.WriteLine($"Created: {corporateBond.Symbol} - ${corporateBond.CurrentPrice} (Coupon: {corporateBond.CouponRate}%)");
        
        // b) Implement buy/sell logic
        Console.WriteLine("\n--- Portfolio Trading ---");
        
        var portfolio = new Portfolio<IFinancialInstrument>();
        var purchasePrices = new Dictionary<IFinancialInstrument, decimal>();
        
        // Buy stocks
        portfolio.Buy(apple, 10, 150m);
        purchasePrices[apple] = 150m;
        Console.WriteLine($"✓ Bought 10 shares of {apple.Symbol} @ ${150} = ${1500}");
        
        portfolio.Buy(microsoft, 5, 310m);
        purchasePrices[microsoft] = 310m;
        Console.WriteLine($"✓ Bought 5 shares of {microsoft.Symbol} @ ${310} = ${1550}");
        
        // Buy bonds
        portfolio.Buy(governmentBond, 10, 98.5m);
        purchasePrices[governmentBond] = 98.5m;
        Console.WriteLine($"✓ Bought 10 units of {governmentBond.Symbol} @ ${98.5} = ${985}");
        
        portfolio.Buy(corporateBond, 8, 95.0m);
        purchasePrices[corporateBond] = 95.0m;
        Console.WriteLine($"✓ Bought 8 units of {corporateBond.Symbol} @ ${95} = ${760}");
        
        // Calculate initial portfolio value
        decimal initialValue = portfolio.CalculateTotalValue();
        Console.WriteLine($"\nInitial Portfolio Value: ${initialValue}");
        
        // c) Create trading strategy with lambda conditions
        Console.WriteLine("\n--- Trading Strategy ---");
        
        var strategy = new TradingStrategy<IFinancialInstrument>();
        
        // Buy condition: price < purchase price (undervalued)
        Func<IFinancialInstrument, bool> buyCondition = (instrument) =>
        {
            if (purchasePrices.ContainsKey(instrument))
                return instrument.CurrentPrice < purchasePrices[instrument] * 0.95m; // 5% discount
            return false;
        };
        
        // Sell condition: price > purchase price (good profit)
        Func<IFinancialInstrument, bool> sellCondition = (instrument) =>
        {
            if (purchasePrices.ContainsKey(instrument))
                return instrument.CurrentPrice > purchasePrices[instrument] * 1.10m; // 10% gain
            return false;
        };
        
        Console.WriteLine("Strategy: Buy when 5% below purchase price, Sell when 10% above");
        
        // d) Track price history
        Console.WriteLine("\n--- Price History Tracking ---");
        
        var priceHistory = new PriceHistory<IFinancialInstrument>();
        DateTime baseDate = DateTime.Now.AddDays(-10);
        
        // Simulate historical prices for Apple
        decimal[] applePrices = { 145m, 147m, 148m, 150m, 152m, 153m, 155m, 157m, 158m, 160m };
        for (int i = 0; i < applePrices.Length; i++)
        {
            priceHistory.AddPrice(apple, baseDate.AddDays(i), applePrices[i]);
        }
        
        // Simulate historical prices for Microsoft
        decimal[] msPrices = { 305m, 308m, 310m, 312m, 315m, 318m, 320m, 322m, 325m, 328m };
        for (int i = 0; i < msPrices.Length; i++)
        {
            priceHistory.AddPrice(microsoft, baseDate.AddDays(i), msPrices[i]);
        }
        
        Console.WriteLine($"Added {applePrices.Length} daily prices for {apple.Symbol}");
        Console.WriteLine($"Added {msPrices.Length} daily prices for {microsoft.Symbol}");
        
        // e) Demonstrate functionality
        
        // Portfolio rebalancing
        Console.WriteLine("\n--- Portfolio Rebalancing ---");
        
        // Update current prices
        apple.CurrentPrice = 160m;
        microsoft.CurrentPrice = 328m;
        
        Console.WriteLine($"Updated prices - {apple.Symbol}: ${apple.CurrentPrice}, {microsoft.Symbol}: ${microsoft.CurrentPrice}");
        
        decimal updatedValue = portfolio.CalculateTotalValue();
        Console.WriteLine($"Updated Portfolio Value: ${updatedValue}");
        decimal portfolioGain = updatedValue - initialValue;
        Console.WriteLine($"Portfolio Gain/Loss: ${portfolioGain} ({(portfolioGain / initialValue * 100):F2}%)");
        
        // Risk calculation
        Console.WriteLine("\n--- Risk Metrics ---");
        
        IFinancialInstrument[] instruments = new IFinancialInstrument[] { apple, microsoft, governmentBond, corporateBond };
        var riskMetrics = strategy.CalculateRiskMetrics(instruments);
        
        foreach (var metric in riskMetrics)
        {
            Console.WriteLine($"  {metric.Key}: {metric.Value:F4}");
        }
        
        // Trend detection
        Console.WriteLine("\n--- Trend Detection ---");
        
        var appleTrend = priceHistory.DetectTrend(apple, 5);
        var msTrend = priceHistory.DetectTrend(microsoft, 5);
        
        Console.WriteLine($"{apple.Symbol} Trend (5-day): {appleTrend}");
        Console.WriteLine($"{microsoft.Symbol} Trend (5-day): {msTrend}");
        
        // Moving averages
        var appleMA = priceHistory.GetMovingAverage(apple, 5);
        var msMA = priceHistory.GetMovingAverage(microsoft, 5);
        
        if (appleMA.HasValue)
            Console.WriteLine($"{apple.Symbol} 5-day Moving Average: ${appleMA:F2}");
        if (msMA.HasValue)
            Console.WriteLine($"{microsoft.Symbol} 5-day Moving Average: ${msMA:F2}");
        
        // Performance comparison
        Console.WriteLine("\n--- Performance Comparison ---");
        
        var topPerformer = portfolio.GetTopPerformer(purchasePrices);
        if (topPerformer.HasValue)
        {
            var (instrument, returnPct) = topPerformer.Value;
            Console.WriteLine($"Top Performer: {instrument.Symbol}");
            Console.WriteLine($"Return: {returnPct:F2}%");
        }
        
        // Individual returns
        foreach (var instrument in new IFinancialInstrument[] { apple, microsoft, governmentBond, corporateBond })
        {
            if (purchasePrices.ContainsKey(instrument))
            {
                decimal purchasePrice = purchasePrices[instrument];
                decimal returnPct = ((instrument.CurrentPrice - purchasePrice) / purchasePrice) * 100;
                string status = returnPct >= 0 ? "✓" : "✗";
                Console.WriteLine($"{status} {instrument.Symbol}: {returnPct:F2}% (Bought @ ${purchasePrice}, Current: ${instrument.CurrentPrice})");
            }
        }
        
        Console.WriteLine("\n========== END OF SIMULATION ==========");
    }
}
