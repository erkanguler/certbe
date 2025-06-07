
USE Certification; 

DROP TABLE IF EXISTS [Certification].[dbo].[Booking];
DROP TABLE IF EXISTS [Certification].[dbo].[Exam_Date];
DROP TABLE IF EXISTS [Certification].[dbo].[Cert];
DROP TABLE IF EXISTS [Certification].[dbo].[Customer];
DROP TABLE IF EXISTS [Certification].[dbo].[Category];

CREATE TABLE [Certification].[dbo].[Customer] (
    [id]         INT            IDENTITY (1, 1) NOT NULL,
    [first_name] NVARCHAR (80)  NOT NULL,
    [last_name]  NVARCHAR (80)  NOT NULL,
    [email]      NVARCHAR (150) NOT NULL,
    [password]   NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [Certification].[dbo].[Category] (
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    [name]          NVARCHAR (200)  NOT NULL,
    [description]   NVARCHAR(max)   NOT NULL,
    [image]         NVARCHAR (150)  NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([id] ASC)
);

CREATE TABLE [dbo].[Cert] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [category_id] INT             NOT NULL,
    [name]        NVARCHAR (150)  NOT NULL,
    [price]       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Cert] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Cert_Category] FOREIGN KEY ([category_id]) REFERENCES [dbo].[Category] ([id])
);

CREATE TABLE [dbo].[Exam_Date] (
    [id]      INT      IDENTITY (1, 1) NOT NULL,
    [cert_id] INT      NOT NULL,
    [startt]  DATETIME NOT NULL,
    [endd]    DATETIME NOT NULL,
    CONSTRAINT [PK_Exam_Date] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Exam_Date_Cert] FOREIGN KEY ([cert_id]) REFERENCES [dbo].[Cert] ([id])
);


CREATE TABLE [dbo].[Booking] (
    [id]           INT IDENTITY (1, 1) NOT NULL,
    [customer_id]  INT NOT NULL,
    [exam_date_id] INT NOT NULL,
    CONSTRAINT [PK_Booking] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Booking_Customer] FOREIGN KEY ([customer_id]) REFERENCES [dbo].[Customer] ([id]),
    CONSTRAINT [FK_Booking_Exam_Date] FOREIGN KEY ([exam_date_id]) REFERENCES [dbo].[Exam_Date] ([id])
);





USE Certification;
INSERT INTO Customer (first_name, last_name, email, password)
VALUES (N'Emma', N'Andersson', N'emma@gmail.com', 'emma123');

INSERT INTO Customer (first_name, last_name, email, password)
VALUES (N'Emil', N'Svensson', N'emil@gmail.com', 'emil123');

INSERT INTO Category (name, description, image)
VALUES (N'Microsoft Office Specialist', 
N'Certifieringstest ger bevis på kompetens som både arbetsgivare och medarbetare har nytta av. För företag är kunskapstest och certifiering ett bra sätt att mäta utbildningsbehov och resultat. Med certifierad personal minskar supportkostnader eftersom produktivitet och tekniska kunskaper ökar, dessutom är certifierad personal en kvalitetsgaranti för kunder. För medarbetaren innebär ett certifikat och en certifiering ett erkännande av specialistkunskaper. Microsoft certifieringar hos Certiport och kan tas i något av Lexicons testcenter. Varje MOS (Microsoft Office Specialist) certifiering är en timme långt. Testerna genomförs praktiskt i de olika Officeprogrammen, så kallat live in the app.',
N'/path/to/image');

INSERT INTO Cert (category_id, name, price)
VALUES 
(1, N'Word 365/2019 Associate', 1500.00),
(1, N'Excel 365/2019 Associate', 1500.00),
(1, N'Outlook 365/2019 Associate', 1500.00),
(1, N'PowerPoint 365/2019 Associate', 1500.00),
(1, N'Word 365/2019 Expert', 1500.00),
(1, N'Excel 365/2019 Expert', 1500.00),
(1, N'Access 365/2019 Expert', 1500.00);

INSERT INTO Category (name, description, image)
VALUES (N'Adobe Certified Professional', 
N'Beskrivning av kursen kommer snart',
N'/path/to/image');

INSERT INTO Cert (category_id, name, price)
VALUES 
(2, N'Adobe After Effects', 1200.00),
(2, N'Adobe Animate', 1200.00),
(2, N'Adobe Dreamweaver', 1200.00),
(2, N'Adobe Illustrator', 1200.00),
(2, N'Adobe InDesign', 1200.00),
(2, N'Adobe Photoshop', 1200.00),
(2, N'Adobe Premiere Pro', 1200.00);

INSERT INTO Exam_Date (cert_id, startt, endd)
VALUES (1, '2025-06-11 09:45', '2025-06-11 10:15');

INSERT INTO Exam_Date (cert_id, startt, endd)
VALUES (6, '2025-06-13 09:45', '2025-06-13 10:15');

INSERT INTO Exam_Date (cert_id, startt, endd)
VALUES (13, '2025-06-19 09:45', '2025-06-19 10:15');

INSERT INTO Booking (customer_id, exam_date_id)
VALUES  (1, 1),
        (2, 2);


UPDATE Category  
SET image = N'/img/microsoft_office_specialist.webp'  
WHERE id = 1;

UPDATE Category  
SET image = N'/img/adobe.png'  
WHERE id = 2;

