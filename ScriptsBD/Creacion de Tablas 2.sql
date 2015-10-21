USE SIPORTBD;

GO


CREATE SCHEMA OPERACIONES AUTHORIZATION siportadm;

GO

CREATE TABLE OPERACIONES.CLIENTE
(
	IDCLIENTES           bigint IDENTITY (1,1) ,
	CODIGOCLIENTE        char(6)  NULL ,
	IDTIPOIDENTIFICACION BIGINT  NULL ,
	TIPOIDENTIFICACION   varchar(2)  NULL ,
	NOMBRERAZONSOCIAL    varchar(200)  NULL ,
	NROIDENTIFICACION    varchar(15)  NULL 
)
go


ALTER TABLE OPERACIONES.CLIENTE
	ADD CONSTRAINT XPKCLIENTE PRIMARY KEY  CLUSTERED (IDCLIENTES ASC)
go


CREATE TABLE OPERACIONES.HORARIOENTREGA
(
	IDHORARIOENTREGA     bigint IDENTITY (1,1) ,
	HORAINICIO           smalldatetime  NULL ,
	HORAFIN              smalldatetime  NULL ,
	FECHACREACION        datetime  NULL ,
	USUARIOCREACION      varchar(20)  NULL ,
	LOGFECHA             datetime  NULL ,
	LOGUSUARIO           varchar(20)  NULL 
)
go


ALTER TABLE OPERACIONES.HORARIOENTREGA
	ADD CONSTRAINT XPKHORARIOENTREGA PRIMARY KEY  CLUSTERED (IDHORARIOENTREGA ASC)
go


CREATE TABLE OPERACIONES.MULTIUSO
(
	IDMULTIUSO           bigint IDENTITY (1,1) ,
	NOMBRE               varchar(50)  NULL ,
	DESCRIPCION          varchar(100)  NULL ,
	VALOR                varchar(10)  NULL ,
	IDPADRE              bigint  NULL ,
	ESTADO               char(2)  NULL ,
	TIPOACCESO           char(1)  NULL ,
	FECHACREACION        datetime  NULL ,
	USUARIOCREACION      varchar(20)  NULL 
)
go


ALTER TABLE OPERACIONES.MULTIUSO
	ADD CONSTRAINT XPKMULTIUSO PRIMARY KEY  CLUSTERED (IDMULTIUSO ASC)
go


CREATE TABLE OPERACIONES.ORDENSERVICIO
(
	IDORDENSERVICIO      bigint IDENTITY (1,1) ,
	CODIGOORDSERVICIO    char(6)  NULL ,
	DESCRIPCION          varchar(100)  NULL ,
	COMENTARIO           varchar(200)  NULL ,
	FECHACREACION        datetime  NULL ,
	USUARIOCREACION      varchar(20)  NULL ,
	LOGFECHA             datetime  NULL ,
	LOGUSUARIO           varchar(20)  NULL ,
	IDCLIENTES           bigint  NULL 
)
go


ALTER TABLE OPERACIONES.ORDENSERVICIO
	ADD CONSTRAINT XPKORDENSERVICIO PRIMARY KEY  CLUSTERED (IDORDENSERVICIO ASC)
go


CREATE TABLE OPERACIONES.ORDENSERVICIODESTINO
(
	IDORDENSERVICIO      bigint  NULL ,
	IDORDENSERVICIODESTINO bigint IDENTITY (1,1) ,
	IDUBIGEO             bigint  NULL ,
	FECHACREACION        datetime  NULL ,
	USUARIOCREACION      varchar(20)  NULL ,
	LOGFECHA             datetime  NULL ,
	LOGUSUARIO           varchar(20)  NULL ,
	FECHAESTENTREGA      datetime  NULL ,
	FECHAREALDESPACHO    datetime  NULL ,
	IDHORARIOENTREGA     bigint  NULL ,
	DIRECCION            varchar(300)  NULL ,
	REFERENCIA           varchar(1000)  NULL ,
	ESTADO               char(2)  NULL 
)
go


ALTER TABLE OPERACIONES.ORDENSERVICIODESTINO
	ADD CONSTRAINT XPKORDENSERVICIODESTINO PRIMARY KEY  CLUSTERED (IDORDENSERVICIODESTINO ASC)
go


CREATE TABLE OPERACIONES.UBIGEO
(
	IDUBIGEO             bigint IDENTITY (1,1) ,
	CODIGOUBIGEO         char(6)  NULL ,
	DESCRIPCION          varchar(150)  NULL ,
	LOGFECHA             datetime  NULL ,
	LOGUSUARIO           varchar(20)  NULL 
)
go


ALTER TABLE OPERACIONES.UBIGEO
	ADD CONSTRAINT XPKUBIGEO PRIMARY KEY  CLUSTERED (IDUBIGEO ASC)
go


CREATE TABLE OPERACIONES.VEHICULO
(
	IDVEHICULO           bigint IDENTITY (1,1) ,
	CODIGOVEHICULO       char(6)  NULL ,
	IDMODELO             bigint  NULL ,
	MODELO               varchar(4)  NULL ,
	NUMEROPLACA          varchar(7)  NULL 
)
go


ALTER TABLE OPERACIONES.VEHICULO
	ADD CONSTRAINT XPKVEHICULO PRIMARY KEY  CLUSTERED (IDVEHICULO ASC)
go



ALTER TABLE OPERACIONES.ORDENSERVICIO
	ADD CONSTRAINT R_CLIENTEORDENSERV FOREIGN KEY (IDCLIENTES) REFERENCES OPERACIONES.CLIENTE(IDCLIENTES)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go



ALTER TABLE OPERACIONES.ORDENSERVICIODESTINO
	ADD CONSTRAINT R_ORDSRVORDSRVDESTINO FOREIGN KEY (IDORDENSERVICIO) REFERENCES OPERACIONES.ORDENSERVICIO(IDORDENSERVICIO)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE OPERACIONES.ORDENSERVICIODESTINO
	ADD CONSTRAINT R_9 FOREIGN KEY (IDUBIGEO) REFERENCES OPERACIONES.UBIGEO(IDUBIGEO)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

ALTER TABLE OPERACIONES.ORDENSERVICIODESTINO
	ADD CONSTRAINT R_10 FOREIGN KEY (IDHORARIOENTREGA) REFERENCES OPERACIONES.HORARIOENTREGA(IDHORARIOENTREGA)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
go

