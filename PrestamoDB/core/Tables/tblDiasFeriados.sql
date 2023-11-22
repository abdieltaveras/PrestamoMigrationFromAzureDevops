CREATE TABLE [core].[tblDiasFeriados] (
    [idDiaFeriado] INT           IDENTITY (1, 1) NOT NULL,
    [Dia]          DATETIME      NOT NULL,
    [Descripcion]  VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([idDiaFeriado] ASC)
);

