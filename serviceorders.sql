CREATE TABLE Users
(
    Id SERIAL PRIMARY KEY,

    Username VARCHAR(100) NOT NULL UNIQUE,

    PasswordHash VARCHAR(500) NOT NULL,

    Role VARCHAR(50) NOT NULL
);

CREATE TABLE Technicians
(
    Id SERIAL PRIMARY KEY,

    FullName VARCHAR(200) NOT NULL,

    Phone VARCHAR(30) NOT NULL,

    Specialty VARCHAR(100) NOT NULL
);

CREATE TABLE Clients
(
    Id SERIAL PRIMARY KEY,

    FullName VARCHAR(200) NOT NULL,

    DocumentNumber VARCHAR(50) NOT NULL UNIQUE,

    Address VARCHAR(300) NOT NULL,

    Phone VARCHAR(30) NOT NULL
);

CREATE TABLE ServiceOrders
(
    Id SERIAL PRIMARY KEY,

    CreatedDate TIMESTAMP NOT NULL,

    Status VARCHAR(50) NOT NULL,

    Description TEXT NOT NULL,

    TechnicianId INTEGER NOT NULL,

    ClientId INTEGER NOT NULL,

    CONSTRAINT FK_ServiceOrders_Technicians
        FOREIGN KEY (TechnicianId)
        REFERENCES Technicians(Id),

    CONSTRAINT FK_ServiceOrders_Clients
        FOREIGN KEY (ClientId)
        REFERENCES Clients(Id)
);



INSERT INTO Users (Username, PasswordHash, Role)
VALUES (
    'admin',
    '$2a$11$ne1DvXtnEIHc1UoJEnq5Gu59m1izzXZ89TRZRJY5szxJwif6GQfAm',
    'Admin'
);

UPDATE Users
SET PasswordHash = '$2a$11$pz1cxBMkc/4NHtf1V7NhBuQ2kn5z/fq0oyUOB3Re2nlTYOwd4ja5O'
WHERE Username = 'admin';


SELECT * FROM Users WHERE Username = 'admin';



INSERT INTO Technicians
(
    FullName,
    Phone,
    Specialty
)
VALUES
('Carlos Gómez','3001111111','Electricista'),

('Juan Pérez','3002222222','Aire acondicionado'),

('Pedro Ramírez','3003333333','Plomería');



INSERT INTO Clients
(
    FullName,
    DocumentNumber,
    Address,
    Phone
)
VALUES
(
    'María López',
    '12345678',
    'Calle 10 # 20-30',
    '3101111111'
),

(
    'Andrés Rodríguez',
    '98765432',
    'Carrera 5 # 15-40',
    '3102222222'
);

INSERT INTO ServiceOrders
(
    CreatedDate,
    Status,
    Description,
    TechnicianId,
    ClientId
)
VALUES
(
    NOW(),
    'Pending',
    'Revisión instalación eléctrica',
    1,
    1
),

(
    NOW(),
    'InProgress',
    'Mantenimiento aire acondicionado',
    2,
    2
);


select * from Users;