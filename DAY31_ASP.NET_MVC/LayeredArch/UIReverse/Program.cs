using BLLRev;
namespace UIReverse
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BLLRevString revString = new BLLRevString();
            string y = revString.ReverseNameBL();
            Console.WriteLine(y);
        }
    }
}
