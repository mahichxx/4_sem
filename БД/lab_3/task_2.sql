USE z_MyBase_2;
GO

CREATE TABLE Виды_кредита (
    Код_вида int PRIMARY KEY,
    Название nvarchar(50) NOT NULL UNIQUE,
    Ставка real NOT NULL CHECK (Ставка > 0) 

CREATE TABLE Клиенты (
    ID_клиента int PRIMARY KEY, 
    ФИО nvarchar(100) NOT NULL, 
    Паспорт nvarchar(20) NOT NULL UNIQUE, 
    Дата_регистрации date DEFAULT GETDATE() 
);

CREATE TABLE Выдача_кредитов (
    Номер_договора int PRIMARY KEY, 
    ID_клиента int NOT NULL, --FOREIGN KEY (ID_клиента) REFERENCES Клиенты(ID_клиента),
    Код_вида int NOT NULL,
    Сумма real NOT NULL CHECK (Сумма > 0), 
    Дата_выдачи date DEFAULT GETDATE(),
    Статус nvarchar(20) DEFAULT N'Оформлен', 

    CONSTRAINT FK_Клиент FOREIGN KEY (ID_клиента) REFERENCES Клиенты(ID_клиента),
    CONSTRAINT FK_ВидКредита FOREIGN KEY (Код_вида) REFERENCES Виды_кредита(Код_вида)
);
GO