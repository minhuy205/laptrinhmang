CREATE DATABASE ChatAppDB;
GO
USE ChatAppDB;
GO
CREATE TABLE UserAccounts (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(255) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    LoginDate DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE Messages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SenderUsername NVARCHAR(255) NOT NULL,
    ReceiverUsername NVARCHAR(255) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    SentAt DATETIME DEFAULT GETDATE()
);
GO
INSERT INTO UserAccounts (Username, Password) VALUES ('admin', '123');
INSERT INTO UserAccounts (Username, Password) VALUES ('user1', '123');
INSERT INTO UserAccounts (Username, Password) VALUES ('user2', '123');
GO
