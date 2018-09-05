create database MetricsApplication

Create table EmployeesTable
(
email varchar(max),
secretPassword varchar(max),
userName varchar(max),
employeeId varchar(max),
applicationName varchar(max)
)
drop table EmployeesTable
alter procedure InsertIntoEmployeesTable(@email varchar(max),@secretPassword varchar(max),@userName varchar(max),@employeeId varchar(max),@applicationName varchar(max)) as
begin 
insert into EmployeesTable values(@email,@secretPassword,@userName,@employeeId,@applicationName)
end

InsertIntoEmployeesTable 'tharundintakurthi@gmail.com','tharundintakurthi@gmail.com','Tharun','2344153','cosmos new'
InsertIntoEmployeesTable 'th@gmail.com','th','Thau','2344153','cosmos new'

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

alter procedure AllMetricsGetter as
begin
select id,applicationName,taskDescription,taskClassification,cast(startDate as date) as startDate,CAST(endDate as date) as endDate,effortHours,userName from MetricsData order by userName
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
noOfDays int,
vacationType varchar(max),
comment varchar(max)
)
drop table leavedetails
alter procedure InsertIntoLeaveDetails
(
@username varchar(max),
@startDate datetime,
@endDate datetime,
@noOfDays int,
@vacationType varchar(max),
@comment varchar(max)
)as
begin
insert into LeaveDetails values(@username,@startDate,@endDate,@noOfDays,@vacationType,@comment)
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
totalHours int,
manuallyFilled varchar(max),
)

drop table CATWHours

alter procedure InsertIntoCATWHours
(
@userName varchar(max),
@totalHours int,
@manuallyFilled varchar(max)
)as
begin
insert into CATWHours values(@userName,@totalHours,@manuallyFilled)
end

insert into CATWHours values('Tharun',0,'no')

alter procedure updateCATWHours(@userName varchar(max),@totalHours int,@manuallyFilled varchar(max))as
begin
update CATWHours set totalHours=@totalHours,manuallyFilled=@manuallyFilled where userName=@userName
end

create procedure GetCatwHoursByUserName(@userName varchar(max))as
begin
select totalHours from CATWHours where userName=@userName
end

create procedure CatwFilled(@userName varchar(max))as
begin
select * from CATWHours where username=@username and manuallyFilled='Yes'
end


create procedure AllLeaveDetailsGetter as begin
select ed.applicationName,ed.username,cast(ld.StartDate as date) as StartDate,cast(ld.EndDate as date) as EndDate,ld.noOfDays,ld.vacationType,ld.comment
 from employeesTable ed 
join LeaveDetails ld on ed.userName=ld.username
end

select * from LeaveDetails

alter procedure Allstatussheet as begin
select distinct ed.employeeid,ed.username,ed.applicationname,ch.totalhours from employeesTable ed
 join catwhours ch on ed.userName=ch.username
end

select  distinct ed.username from employeestable ed
 join MetricsData md on
 not(ed.username=md.userName) join LeaveDetails ld on not(md.userName=ld.username)


 alter procedure AllEmployees as
 begin
 select userName,email,applicationName from employeestable
 end
 CatwFilled 'Tharun'

 delete from employeestable
  delete from leavedetails
   delete from catwhours