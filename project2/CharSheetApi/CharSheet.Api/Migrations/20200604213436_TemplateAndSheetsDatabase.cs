using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CharSheet.Api.Migrations
{
    public partial class TemplateAndSheetsDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormPositions",
                columns: table => new
                {
                    FormPostionId = table.Column<Guid>(nullable: false),
                    FormTemplateId = table.Column<Guid>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormPositions", x => x.FormPostionId);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: true),
                    IterationCount = table.Column<int>(nullable: false),
                    Hashed = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "Sheets",
                columns: table => new
                {
                    SheetId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.SheetId);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    TemplateId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.TemplateId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginId = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    FormTemplateId = table.Column<Guid>(nullable: false),
                    TemplateId = table.Column<Guid>(nullable: false),
                    FormPositionId = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.FormTemplateId);
                    table.ForeignKey(
                        name: "FK_FormTemplates_FormPositions_FormPositionId",
                        column: x => x.FormPositionId,
                        principalTable: "FormPositions",
                        principalColumn: "FormPostionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormTemplates_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "TemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInputGroups",
                columns: table => new
                {
                    FormInputGroupId = table.Column<Guid>(nullable: false),
                    SheetId = table.Column<Guid>(nullable: false),
                    FormTemplateId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInputGroups", x => x.FormInputGroupId);
                    table.ForeignKey(
                        name: "FK_FormInputGroups_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormInputGroups_Sheets_SheetId",
                        column: x => x.SheetId,
                        principalTable: "Sheets",
                        principalColumn: "SheetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormLabels",
                columns: table => new
                {
                    FormLabelId = table.Column<Guid>(nullable: false),
                    FormTemplateId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormLabels", x => x.FormLabelId);
                    table.ForeignKey(
                        name: "FK_FormLabels_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "FormTemplateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormInputs",
                columns: table => new
                {
                    FormInputId = table.Column<Guid>(nullable: false),
                    FormInputGroupId = table.Column<Guid>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormInputs", x => x.FormInputId);
                    table.ForeignKey(
                        name: "FK_FormInputs_FormInputGroups_FormInputGroupId",
                        column: x => x.FormInputGroupId,
                        principalTable: "FormInputGroups",
                        principalColumn: "FormInputGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormInputGroups_FormTemplateId",
                table: "FormInputGroups",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInputGroups_SheetId",
                table: "FormInputGroups",
                column: "SheetId");

            migrationBuilder.CreateIndex(
                name: "IX_FormInputs_FormInputGroupId",
                table: "FormInputs",
                column: "FormInputGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FormLabels_FormTemplateId",
                table: "FormLabels",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplates_FormPositionId",
                table: "FormTemplates",
                column: "FormPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormTemplates_TemplateId",
                table: "FormTemplates",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LoginId",
                table: "Users",
                column: "LoginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormInputs");

            migrationBuilder.DropTable(
                name: "FormLabels");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FormInputGroups");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "FormTemplates");

            migrationBuilder.DropTable(
                name: "Sheets");

            migrationBuilder.DropTable(
                name: "FormPositions");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
