namespace Policies.Core.Helpers
{
    public static class SqlQueries
    {
        public static class Auth
        {
           
            public const string ValidateAccess = @"
            SELECT 1 
            FROM Users u
            INNER JOIN UserClients uc ON u.Id = uc.UserId
            INNER JOIN Policies p ON p.ClientId = uc.ClientId
            WHERE u.Id = @UserId AND p.Id = @PoliciesId";
            
            public const string CheckUser = @"
            SELECT Username FROM Users WHERE id = @Id";
            
            public const string CheckPass = @"
            SELECT Password FROM Users WHERE Username = @Username";
        }

        public static class Client
        {
            public const string CreateClient = @"
            INSERT INTO Clients (
                ExId, Name, FirstLastName, SecondLastName, 
                Age, BirthCountry, Gender, Email, Phone, CreatedAt,  UpdatedAt)
            VALUES (
                NEWID(), @Name, @FirstLastName, @SecondLastName,
                @Age, @BirthCountry, @Gender, @Email, @Phone, GETDATE(), GETDATE());
            
            SELECT SCOPE_IDENTITY();";

            public const string UpdateClient = @"
            UPDATE Clients
            SET
                Name = @Name,
                FirstLastName = @FirstLastName,
                SecondLastName = @SecondLastName,
                Age = @Age,
                BirthCountry = @BirthCountry,
                Gender = @Gender,
                Email = @Email,
                Phone = @Phone,
                UpdatedAt = GETDATE()
            WHERE
                Id = @ClientId;";

            public const string GetClients = @"
            SELECT * FROM Clients";
            
            public const string GetClientById = @"
            SELECT * FROM Clients WHERE Id = @clientId";

            public const string GetClientByUser = @"
            SELECT * FROM Clients WHERE Email = @email";


        }

        public static class Policies
        {
          
            public const string GetById = @"
            SELECT p.*, c.*
            FROM Policies p
            INNER JOIN Clients c ON p.ClientId = c.Id
            WHERE p.PolicyNumber = @Id";

            public const string Update = @"
            UPDATE Policies 
            SET Type = @Type,
                StartDate = @StartDate,
                ClientId = @ClientId,
                EndDate = @EndDate,
                Amount = @Amount,
                UpdatedAt = GETDATE()
            WHERE PolicyNumber = @Id";

            public const string Authorize = @"
            UPDATE Policies 
            SET Status = 'Autorizada',
                UpdatedAt = GETDATE()
            WHERE PolicyNumber = @Id";

            public const string Reject = @"
            UPDATE Policies 
            SET Status = 'Rechazada',
                UpdatedAt = GETDATE()
            WHERE PolicyNumber = @Id";

            public const string Delete = "DELETE FROM Policies WHERE Id = @Id";
        }

        public static class StoreProcedures
        {
            public const string ValidateUser = "sp_ValidateUser";
            public const string ChangePassword = "sp_ChangePassword";
            public const string GetAll = "sp_GetPoliciesWithClient";
            public const string CreatePolicy = "sp_CreatePolicy";
            public const string CreateUser = "sp_CreateUser";
        }
    }
}