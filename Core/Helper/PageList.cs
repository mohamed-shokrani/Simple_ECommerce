using Microsoft.EntityFrameworkCore;

namespace Core.Helper;

public class PageList<T> :List<T>//make it generic so it can take any entity like MemberDTO
{
    public PageList(IEnumerable<T>  items,int count, int pageNumber, int pageSize)
    {//inside this constructor we are going to parse in the item that we get from our query type Ienumerable 
        CurrentPage = pageNumber;
        TotalPage = (int)Math.Ceiling(count / (double)pageSize);
 // TotalPage = //if we have got a total count of 10 and our page size 5 then we get 2 pages from our query
 // if we have a count of 9 then we are gonna  work count 9/5 but ceiling will make it in two pages;
        PageSize = pageSize;
        TotatCount = count;
        AddRange(items);//add the range of the itmes inside this ctor so we have access to these items inside our page list ctor
         
    }

    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set;}

   public int TotatCount { get; set; }
    public static async Task<PageList<T>> CreateAsync(IQueryable<T> sourceData, int pageNumber, int pageSize)
    {

        var count = await sourceData.CountAsync();
        var items =await sourceData.Skip((pageNumber -1)*pageSize).Take(pageSize).ToListAsync();
        return new PageList<T>(items, count, pageNumber, pageSize);

    } 

}
