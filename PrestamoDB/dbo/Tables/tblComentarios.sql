CREATE TABLE [dbo].[tblComentarios] (
    [IdComentario]       INT            IDENTITY (1, 1) NOT NULL,
    [IdTransaccion]      INT            NOT NULL,
    [TablaOrigen]        VARCHAR (50)   NOT NULL,
    [Detalle]            VARCHAR (1000) NOT NULL,
    [IdNegocio]          INT            NOT NULL,
    [IdLocalidadNegocio] INT            NOT NULL,
    [InsertadoPor]       VARCHAR (100)  NOT NULL,
    [FechaInsertado]     DATETIME       NOT NULL,
    [ModificadoPor]      VARCHAR (100)  NULL,
    [FechaModificado]    DATETIME       NULL,
    [BorradoPor]         VARCHAR (100)  NULL,
    [FechaBorrado]       DATETIME       NULL,
    CONSTRAINT [PK_Comentarios] PRIMARY KEY CLUSTERED ([IdComentario] ASC)
);

