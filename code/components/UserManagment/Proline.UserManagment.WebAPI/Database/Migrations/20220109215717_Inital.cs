using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Proline.Component.UserManagment.Web.Service.Database.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instance",
                columns: table => new
                {
                    InstanceId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    InstanceIdentityId = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    LastSeenOnlineAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IpAddress = table.Column<string>(nullable: true),
                    IsWhitelisted = table.Column<bool>(nullable: false),
                    MaxPlayers = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instance", x => x.InstanceId);
                });

            migrationBuilder.CreateTable(
                name: "InstancePlayer",
                columns: table => new
                {
                    InstancePlayerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(nullable: false),
                    InstanceId = table.Column<long>(nullable: false),
                    LastSeenAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstancePlayer", x => x.InstancePlayerId);
                });

            migrationBuilder.CreateTable(
                name: "InstanceUserAllow",
                columns: table => new
                {
                    InstanceAllowId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    AllowedAt = table.Column<DateTime>(nullable: false),
                    ExpiresAt = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceUserAllow", x => x.InstanceAllowId);
                });

            migrationBuilder.CreateTable(
                name: "InstanceUserDeny",
                columns: table => new
                {
                    InstanceDenyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    BannedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceUserDeny", x => x.InstanceDenyId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerAccount",
                columns: table => new
                {
                    PlayerId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerAccount", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerIndentity",
                columns: table => new
                {
                    IdentityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<long>(nullable: false),
                    IdentityTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    Identifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerIndentity", x => x.IdentityId);
                });

            migrationBuilder.CreateTable(
                name: "PlayerIndentityType",
                columns: table => new
                {
                    IdentityTypeId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerIndentityType", x => x.IdentityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserAccount",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<long>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Priority = table.Column<int>(nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccount", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserAllow",
                columns: table => new
                {
                    AllowId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(nullable: false),
                    AllowedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAllow", x => x.AllowId);
                });

            migrationBuilder.CreateTable(
                name: "UserDeny",
                columns: table => new
                {
                    DenyId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeniedByUserId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    BannedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDeny", x => x.DenyId);
                });

            migrationBuilder.CreateTable(
                name: "UserInstanceLicence",
                columns: table => new
                {
                    InstanceIdentityId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInstanceLicence", x => x.InstanceIdentityId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instance");

            migrationBuilder.DropTable(
                name: "InstancePlayer");

            migrationBuilder.DropTable(
                name: "InstanceUserAllow");

            migrationBuilder.DropTable(
                name: "InstanceUserDeny");

            migrationBuilder.DropTable(
                name: "PlayerAccount");

            migrationBuilder.DropTable(
                name: "PlayerIndentity");

            migrationBuilder.DropTable(
                name: "PlayerIndentityType");

            migrationBuilder.DropTable(
                name: "UserAccount");

            migrationBuilder.DropTable(
                name: "UserAllow");

            migrationBuilder.DropTable(
                name: "UserDeny");

            migrationBuilder.DropTable(
                name: "UserInstanceLicence");
        }
    }
}
