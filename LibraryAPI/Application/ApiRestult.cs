

using Microsoft.EntityFrameworkCore;

namespace LibraryApp.API.Application {

    public class ApiResult<T> {

    /// <summary>
    ///  Collection of data that should be sent
    ///  </summary>
    public List<T> Data { get; private set;}
   
    /// <summary>
    ///  Zero-based index of the current page
    /// </summary>
    public int PageIndex { get; private set;}

    /// <summary>
    ///  Number of items on the page
    ///  </summary>
    public int PageSize { get; private set; }

    /// <summary>
    ///  Total number of items in the collection
    ///  </summary>
    public int TotalItems { get; private set; }

    /// <summary>
    ///  Total number of pages
    ///  </summary>
    public int TotalPages { get; private set; }


    private ApiResult(List<T> data, int count, int pageIndex, int pageSize){
         Data=data;
         TotalItems=count;
         PageIndex=pageIndex;
         PageSize=pageSize;
         TotalPages = (int)Math.Ceiling(count / (double)pageSize);
    }

    /// <summary>
    /// Pages a IQueryable source.
    /// </summary>
    /// <param name="source">An IQueryable source of generic type</param>
    /// <param name="pageIndex">Zero-based current page index (0 = first page)</param>
    /// <param name="pageSize">The actual size of each page</param>
    /// <returns>
    ///   Paged data and information needed for correct paging
    /// </return>

    public static async Task<ApiResult<T>> CreateAsync(IQueryable<T> data, int pageIndex, int pageSize){

        //Total number
        int count=await data.CountAsync();

        //Only entities on the requested page
        List<T> resultData=await data.Skip(pageIndex*pageSize).Take(pageSize).ToListAsync();

        return new ApiResult<T>(resultData, count, pageIndex, pageSize);
    }


    /// <summary>
    ///   True if previous page exists, false otherwise
    /// </summary>
    public bool hasPreviousPage() => PageIndex>0;

    /// <summary>
    ///   True if next page exists, false otherwise
    /// </summary>

    public bool hasNextPage() => (PageIndex+1)<TotalPages;

    }

}