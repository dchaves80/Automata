using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class price_record 
{
    public string resourceName;
    public int price;
}


[Serializable]
    public class response_Price:BaseResponse
    {
    public List<price_record> records;

    }

