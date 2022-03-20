-- CREATE TABLE dbo.Members(
--     MemberNo varchar(10) PRIMARY KEY,
--     JoinDate DATETIME NOT NULL,
--     ExpiredDate DATETIME NOT NULL,
--     MemberStatus DateTime NOT NULL,
--     PhoneNo VARCHAR(20) NOT NULL,
--     DWEPackage VARCHAR(20) NOT NULL
-- );
/*
-- Insert rows into table 'TableName'
INSERT INTO dbo.Members
    ( -- columns to insert data into
    MemberNO, JoinDate, ExpiredDate, MemberStatus, PhoneNo, DWEPackage
    )
VALUES
    ( 'S010200', '2019-01-03', '2022-01-03', 'Expired', '0912333550', 'Main' ),
    ( 'S010201', '2019-01-03', '2022-07-04', 'Normal', '0912123551', 'small' ),
    ( 'S010203', '2019-01-03', '2022-07-05', 'Cancel', '0912333551', 'Main' ),
    ( 'S010204', '2021-03-01', '2022-03-01', 'Expired', '0912333552', 'full' ),
    ( 'S010205', '2021-03-01', '2022-03-02', 'Expired', '0912333553', 'full' ),
    ( 'S010208', '2022-01-01', '2023-01-01', 'Normal', '0922210233', 'Main' ),
    ( 'S010209', '2022-01-01', '2023-01-02', 'Cancel', '0922210234', 'Main' ),
    ( 'S010210', '2022-01-01', '2023-01-03', 'Normal', '0922210235', 'Main' )
-- add more rows here
GO
Create TABLE dbo.Customers(
    MemberNo varchar(10) NOT NULL,
    ContractNo varchar(10) PRIMARY KEY,
    SignDate DATETIME NOT NULL,
    ContractStatus varchar(10) NOT NULL,
    PhoneNo VARCHAR(20) NOT NULL,
    DWEPackage VARCHAR(20) NOT NULL
);
INSERT INTO dbo.Customers
    ( -- columns to insert data into
    MemberNO, ContractNo, SignDate, ContractStatus, PhoneNo, DWEPackage
    )
VALUES
    ( 'S010200', 'C020200', '2019-01-03', 'Cancel', '0912333550', 'Main' ),
    ( 'S010201', 'C020201', '2019-01-03', 'Normal', '0912123551', 'small' ),
    ( 'S010200', 'C020202', '2019-01-03', 'Normal', '0912333550', 'full' ),
    ( 'S010203', 'C020203', '2019-01-03', 'Cancel', '0912333551', 'Main' ),
    ( 'S010204', 'C020204', '2021-03-01', 'Normal', '0912333552', 'full' ),
    ( 'S010205', 'C020205', '2021-03-01', 'Normal', '0912333553', 'full' ),
    ( 'S010200', 'C020206', '2021-03-01', 'Cancel', '0912333550', 'Main' ),
    ( 'S010201', 'C020207', '2021-03-01', 'Normal', '0912123551', 'small' ),
    ( 'S010208', 'C020208', '2022-01-01', 'Normal', '0922210233', 'Main' ),
    ( 'S010209', 'C020209', '2022-01-01', 'Cancel', '0922210234', 'Main' ),
    ( 'S010210', 'C020210', '2022-01-01', 'Normal', '0922210235', 'Main' )
-- add more rows here
GO

-- TRUNCATE TABLE dbo.Members
-- DROP TABLE dbo.Customers
-- ALTER TABLE dbo.Members
-- ALTER COLUMN MemberStatus varchar(10);

--SELECT * FROM dbo.Members


SELECT TOP (1000) *
FROM [testdb].[dbo].[Customers]

SELECT TOP (1000) *
FROM [testdb].[dbo].[Members]
/*