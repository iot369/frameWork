﻿CREATE TABLE TB_USER(
	`USER_ID` VARCHAR(50) NOT NULL,
	`PASSWORD` VARCHAR(100) NOT NULL,
	`REAL_NAME` VARCHAR(100) NULL,
	`SEX` VARCHAR(10) NOT NULL,
	`ACCOUNT_STATUS` VARCHAR(20) NOT NULL,
	`EMAIL` VARCHAR(100) NULL,
	`USER_IMAGE` MEDIUMBLOB NULL,
	`USER_IMAGE_PATH` VARCHAR(200) NULL,
	`TITLE` VARCHAR(200) NULL,
PRIMARY KEY  
(
	`USER_ID` 
));

CREATE TABLE TB_PERMISSION(
	PERMISSION_ID VARCHAR(50) NOT NULL,
	PERMISSION_NAME NVARCHAR(50) NOT NULL,
	PARENT_PERMISSION_ID VARCHAR(50) NULL,
PRIMARY KEY  
(
	PERMISSION_ID 
)
) ;

CREATE TABLE TB_ROLE(
	ROLE_ID VARCHAR(50) NOT NULL,
	ROLE_NAME NVARCHAR(50) NOT NULL,
PRIMARY KEY  
(
	ROLE_ID
)
) ;

CREATE TABLE TB_USER_ROLE(
	USER_ID VARCHAR(50) NOT NULL,
	ROLE_ID VARCHAR(50) NOT NULL,
PRIMARY KEY  
(
	USER_ID,
	ROLE_ID 
)
);

CREATE TABLE TB_ROLE_PERMISSION(
	ROLE_ID VARCHAR(50) NOT NULL,
	PERMISSION_ID VARCHAR(50) NOT NULL,
PRIMARY KEY  
(
	ROLE_ID ,
	PERMISSION_ID
)
) ;

CREATE TABLE TB_OP_LOG(
	OP_ID INT AUTO_INCREMENT NOT NULL,
	OP_USER_ID VARCHAR(50) NOT NULL,
	OPER_NAME VARCHAR(100) NOT NULL,
	OPER_IP VARCHAR(50) NOT NULL,
	OPER_TIME DATETIME NOT NULL,
	OPER_DESC VARCHAR(500) NOT NULL,
  PRIMARY KEY  
(
	OP_ID
)
) ;

CREATE TABLE TB_LOGIN_LOG(
	LOG_ID INT AUTO_INCREMENT NOT NULL,
	LOG_USER_ID NVARCHAR(100) NOT NULL,
	LOG_IP VARCHAR(50) NOT NULL,
	LOG_TIME DATETIME NOT NULL,
	LOG_RESULT CHAR(1) NULL,
PRIMARY KEY  
(
	LOG_ID 
)
) ;

INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('01','用户及权限',null);
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0101','权限列表','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0102','权限添加','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0103','权限编辑','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0104','权限删除','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0105','角色列表','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0106','角色添加并分配权限','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0107','角色编辑及编辑权限','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0108','角色删除','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0109','用户列表','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0110','用户添加并分配角色','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0111','用户编辑及编辑角色','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0112','用户删除','01');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('02','日志',null);
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0201','登录日志查看','02');
INSERT INTO TB_PERMISSION(PERMISSION_ID,PERMISSION_NAME,PARENT_PERMISSION_ID) VALUES('0202','操作日志查看','02');

INSERT INTO TB_ROLE(ROLE_ID,ROLE_NAME) VALUES('admin','管理员');

insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','01');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0101');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0102');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0103');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0104');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0105');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0106');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0107');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0108');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0109');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','02');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0201');
insert into TB_ROLE_PERMISSION(ROLE_ID,PERMISSION_ID) values('admin','0202');

INSERT INTO `TB_USER`
           (`USER_ID`
           ,`PASSWORD`
           ,`REAL_NAME`
           ,`SEX`
           ,`ACCOUNT_STATUS`
           ,`EMAIL`
           ,`USER_IMAGE`
           ,`USER_IMAGE_PATH`
           ,`TITLE`)
     VALUES
           ('ADMIN'
           ,'uu6YR6CV/GHRrP7txH82YQ=='
           ,'admin'
           ,'男'
           ,'Y'
           ,'ADMIN@163.COM'
           ,NULL
           ,'avatar5.png'
           ,'工程师');

INSERT INTO TB_USER_ROLE(USER_ID,ROLE_ID) VALUES('admin','admin');

