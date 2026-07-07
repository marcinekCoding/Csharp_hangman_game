//odpowiada za pozostale bledy itp
class Mistakes{
    public int mistakes_made{get; set;} = 0;
    public int possible_mistakes{get;set;}
    public Mistakes(int possible){
        possible_mistakes = possible;
    }

     public void add_mistake()
    {
        mistakes_made++;
    }

     public bool is_mistakes_left()
    {
        if(mistakes_made>=possible_mistakes) return false;
        return true;
    }

};

