using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.DataAccess.Repositories.References
{   
    public static class ReferenceQueryPgSQL
    {
        // SQL Table Names
        public const string INSERT = @"INSERT INTO public.""CovidTest""(""Location"", ""IsLocationSafe"", ""Disease"",""Person"")  
                                    VALUES(@Location, @IsLocationSafe, @Disease,@Person)";


        public const string GET_BY_ID = @"SELECT * FROM public.""CovidTest"" WHERE ""Id""=@Id";
        public const string GET_ALL = @"SELECT * FROM public.""CovidTest""";
        public const string UPDATE = @"UPDATE public.""CovidTest""  SET                                    
                                    ""Location"" = @Location,
                                    ""IsLocationSafe"" = @IsLocationSafe,
                                    ""Disease"" = @Disease,
                                    ""Person"" = @Person
                                    WHERE ""Id"" = @Id";
        public const string DELETE = @"DELETE FROM public.""CovidTest"" WHERE  ""Id""=@Id";

        //Stored Procedure Name
        public const string GET_COVID_TEST_BY_ID = "GetCovidTestById";

    }
}
