using DAL;

namespace BLLRev
{
    public class BLLRevString
    {
        public string ReverseNameBL()
        {
             DALRevCls dalRevCls = new DALRevCls(); 
                string x = dalRevCls.GetAllName();
              x = new string(x.Reverse().ToArray());
            return x;
        }
    }
}
