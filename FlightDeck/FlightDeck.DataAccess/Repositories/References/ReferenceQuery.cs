using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.DataAccess.Repositories.References
{
    public static class ReferenceQuery
    {
        // SQL Table Names
        public const string INSERT = @"INSERT INTO CovidTest(Location, IsLocationSafe, Disease,Person)
                                     OUTPUT INSERTED.*
                                     VALUES(@Location, @IsLocationSafe, @Disease,@Person)";


        public const string GET_BY_ID = @"SELECT * FROM CovidTest WHERE Id=@Id";
        public const string GET_ALL = @"SELECT * FROM CovidTest";
        public const string UPDATE = @"UPDATE CovidTest  SET                                    
                                     Location = @Location,
                                     IsLocationSafe = @IsLocationSafe,
                                     Disease = @Disease,
                                     Person = @Person
                                     OUTPUT INSERTED.*
                                     WHERE Id = @Id";
        public const string DELETE = @"DELETE FROM CovidTest
                                     OUTPUT DELETED.*
                                     WHERE  Id=@Id";


        //Stored Procedure Name
        public const string GET_COVID_TEST_BY_ID = "GetCovidTestById";
    }
}
