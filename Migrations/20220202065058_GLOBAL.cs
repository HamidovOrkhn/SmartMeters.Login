using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartMeterControl.Access_MS.Migrations
{
    public partial class GLOBAL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ROLE_GLOBAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TITLE = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    SITEURL = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    ISHTTPS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    IMAGEURL = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CREATEDDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    UPDATEDDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLE_GLOBAL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_GLOBAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PIN = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SURNAME = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    POSITION = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: false),
                    DEPARTMENTID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    RTOKEN = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    ISACTIVE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATEDDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    UPDATEDDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_GLOBAL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USER_LOG_GL",
                columns: table => new
                {
                    ID = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ACTIONTYPE = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: true),
                    IPADDRESS = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    PERMISSIONID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_LOG_GL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROLEPERM_GLOBAL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ROLEID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    USERID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATEDDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    UPDATEDDATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLEPERM_GLOBAL", x => x.ID);
                    table.ForeignKey(
                        name: "U_ROLE_FK",
                        column: x => x.ROLEID,
                        principalTable: "ROLE_GLOBAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "U_USER_FK",
                        column: x => x.USERID,
                        principalTable: "USER_GLOBAL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ROLEPERM_GLOBAL_ROLEID",
                table: "ROLEPERM_GLOBAL",
                column: "ROLEID");

            migrationBuilder.CreateIndex(
                name: "IX_ROLEPERM_GLOBAL_USERID",
                table: "ROLEPERM_GLOBAL",
                column: "USERID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_GLOBAL_PIN",
                table: "USER_GLOBAL",
                column: "PIN",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ROLEPERM_GLOBAL");

            migrationBuilder.DropTable(
                name: "USER_LOG_GL");

            migrationBuilder.DropTable(
                name: "ROLE_GLOBAL");

            migrationBuilder.DropTable(
                name: "USER_GLOBAL");
        }
    }
}
