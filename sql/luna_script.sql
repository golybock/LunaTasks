create database luna_tasks;

\c luna_tasks;

create table if not exists page
(
    id                uuid                                   not null
        primary key,
    name              varchar(150)                           not null,
    description       varchar(500),
    header_image      text,
    created_timestamp timestamp with time zone default now() not null,
    created_user_id   uuid                                   not null,
    workspace_id      uuid                                   not null,
    deleted           boolean                  default false not null
);

create table if not exists role
(
    id   serial       not null primary key,
    name varchar(150) not null unique
);

create table if not exists status
(
    id           uuid                  not null primary key,
    name         varchar(150)          not null,
    hex_color    varchar(7)            not null,
    workspace_id uuid                  not null,
    deleted      boolean default false not null
);

create table if not exists tag
(
    id           uuid                  not null
        primary key,
    name         varchar(150)          not null,
    hex_color    varchar(7)            not null,
    workspace_id uuid                  not null,
    deleted      boolean default false not null
);

create table if not exists type
(
    id           uuid                  not null primary key,
    name         varchar(150)          not null,
    hex_color    varchar(7)            not null,
    workspace_id uuid                  not null,
    deleted      boolean default false not null
);

create table if not exists card
(
    id                uuid                                   not null
        primary key,
    header            varchar(300)                           not null,
    content           text,
    description       text,
    card_type_id      uuid                                   not null
        references type,
    page_id           uuid                                   not null references page,
    created_user_id   uuid                                   not null,
    created_timestamp timestamp with time zone default now() not null,
    deadline          timestamp with time zone,
    previous_card_id  uuid references card,
    deleted           boolean                  default false not null
);

create table if not exists block_card
(
    id                    serial
        primary key,
    card_id               uuid                                   not null
        references card,
    comment               text,
    blocked_user_id       uuid                                   not null,
    start_block_timestamp timestamp with time zone default now() not null,
    end_block_timestamp   timestamp with time zone
);

create table if not exists comment
(
    id             serial                not null primary key,
    card_id        uuid                  not null references card,
    user_id        uuid                  not null,
    comment        text,
    attachment_url text,
    deleted        boolean default false not null
);

create table if not exists card_status
(
    id            serial
        primary key,
    card_id       uuid                    not null
        references card,
    status_id     uuid                    not null
        references status,
    set_timestamp timestamp default now() not null
);

create table if not exists card_tags
(
    id      serial
        primary key,
    card_id uuid                  not null
        references card,
    tag_id  uuid                  not null
        references tag,
    deleted boolean default false not null
);

create table if not exists card_users
(
    id      serial
        primary key,
    card_id uuid                  not null
        references card,
    user_id uuid                  not null,
    deleted boolean default false not null
);

\c postgres;

create database luna_users;

\c luna_users;

create table if not exists users
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

\c postgres;

create database luna_workspace;

\c luna_workspace;

create table if not exists workspace
(
    id                uuid                                   not null
        primary key,
    name              varchar(150)                           not null,
    created_timestamp timestamp with time zone default now() not null,
    created_user_id   uuid                                   not null
);

create table if not exists workspace_users
(
    id           serial
        primary key,
    user_id      uuid not null unique,
    workspace_id uuid not null references workspace
);

\c postgres;

create database luna_auth;

\c luna_auth;

create table if not exists user_auth
(
    id       uuid primary key,
    user_id  uuid  not null unique,
    password bytea not null
);

\c postgres;

create database luna_files;

\c luna_files;

create table if not exists files
(
    id               uuid primary key,
    path             text        not null,
    fileType         int         not null,
    workspaceId      uuid        not null,
    uploadedByUserId uuid        not null,
    uploadedDate     timestamptz not null default now(),
    deleted          boolean     not null default false
);

\c postgres;

create database luna_notifications;

\c luna_notifications;

create table if not exists notification
(
    id          uuid primary key,
    userId      uuid        not null,
    text        text,
    created     timestamptz not null default now(),
    createdUser uuid        not null,
    priority    int         not null,
    imageUrl    text        null,
    read        boolean     not null default false,
    readAt      timestamptz
);

\c postgres;