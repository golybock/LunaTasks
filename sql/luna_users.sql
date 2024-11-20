create database luna_users;

create table users
(
    id                uuid                                   not null
        primary key,
    username          varchar(100)                           not null
        unique,
    email             varchar(250)                           not null
        unique,
    phone_number      varchar(25),
    created_timestamp timestamp with time zone default now() not null,
    email_confirmed   boolean                  default false not null,
    image             text
);