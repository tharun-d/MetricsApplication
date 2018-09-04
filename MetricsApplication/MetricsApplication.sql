create database MetricsApplication

Create table EmployeesTable
(
email varchar(max),
secretPassword varchar(max),
userName varchar(max),
employeeId varchar(max)
)

create procedure InsertIntoEmployeesTable(@email varchar(max),@secretPassword varchar(max),@userName varchar(max),@employeeId varchar(max)) as
begin 
insert into EmployeesTable values(@email,@secretPassword,@userName,@employeeId)
end

InsertIntoEmployeesTable 'tharundintakurthi@gmail.com','tharundintakurthi@gmail.com','Tharun','2344153'
InsertIntoEmployeesTable 'th@gmail.com','th','Thau','2344153'

create procedure LoginValidator(@email varchar(max),@secretPassword varchar(max)) as
begin
select userName from EmployeesTable where email=@email and secretPassword=@secretPassword
end

create table MetricsData
(
id int identity(1,1),
userName varchar(max),
applicationName varchar(max),
taskDescription varchar(max),
taskClassification varchar(max),
startDate datetime,
endDate datetime,
effortHours float
)

drop table MetricsData

alter procedure InsertIntoMetricsData
(
@userName varchar(max),
@applicationName varchar(max),
@taskDescription varchar(max),
@taskClassification varchar(max),
@startDate datetime,
@endDate datetime,
@effortHours float
)as
begin
insert into MetricsData values(@userName,@applicationName,@taskDescription,@taskClassification,@startDate,@endDate,@effortHours)
end

create procedure AllMetricsGetter as
begin
select * from MetricsData
end
alter procedure AllMetricsGetterbyUserName(@userName varchar(max)) as
begin
select id,applicationName,taskDescription,taskClassification,cast(startDate as date) as startDate,CAST(endDate as date) as endDate,effortHours from MetricsData where userName=@userName
end
alter procedure MetricsGetterbyId(@id int) as
begin
select id,applicationName,taskDescription,taskClassification,cast(startDate as date) as startDate,CAST(endDate as date) as endDate,effortHours from MetricsData where id=@id
end
create procedure MetricsDeletebyId(@id int) as
begin
Delete from MetricsData where id=@id
end

create table LeaveDetails
(
id int identity(1,1),
username varchar(max),
startDate datetime,
endDate datetime,
vacationType varchar(max),
comment varchar(max)
)

create procedure InsertIntoLeaveDetails
(
@username varchar(max),
@startDate datetime,
@endDate datetime,
@vacationType varchar(max),
@comment varchar(max)
)as
begin
insert into LeaveDetails values(@username,@startDate,@endDate,@vacationType,@comment)
end

alter procedure AllLeaveDetailsbyUserName(@userName varchar(max)) as
begin
select id,cast(startDate as date) as startDate,cast(endDate as date) as endDate,vacationType,comment from LeaveDetails where username=@userName
end

create procedure DeleteLeaveDetailsByid(@id int) as
begin
Delete from LeaveDetails where id=@id
end
select * from metricsdata

create table CATWHours
(
userName varchar(max),
totalHours int
)

create procedure InsertIntoCATWHours
(
@userName varchar(max),
@totalHours int
)as
begin
insert into CATWHours values(@userName,@totalHours)
end

insert into CATWHours values('Tharun',0)

create procedure updateCATWHours(@userName varchar(max),@totalHours int)as
begin
update CATWHours set totalHours=@totalHours where userName=@userName
end

create procedure GetCatwHoursByUserName(@userName varchar(max))as
begin
select totalHours from CATWHours where userName=@userName
end