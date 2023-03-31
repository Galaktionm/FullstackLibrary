
using System.Diagnostics.CodeAnalysis;

namespace LibraryApp.API.Services {

    public class HashSetEqualityComparer<T> : EqualityComparer<HashSet<T>>
    {
        public override bool Equals(HashSet<T>? x, HashSet<T>? y)
        {
            if(x!=null && y!=null){
                return x.SetEquals(y);
            } 
            return false;
        }

        public override int GetHashCode([DisallowNull] HashSet<T> obj)
        {
            int result=0;
            foreach(var instance in obj){
                result+=instance.GetHashCode();
            }
            return result;
        }
    }

}