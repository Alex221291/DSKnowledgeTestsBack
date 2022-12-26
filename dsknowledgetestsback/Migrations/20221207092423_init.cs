using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dsknowledgetestsback.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeedbackCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_FeedbackCategories_FeedbackCategoryId",
                        column: x => x.FeedbackCategoryId,
                        principalTable: "FeedbackCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("8a0d4761-f154-46a3-bc13-d29828ec7dc2"), "Basic" },
                    { new Guid("9d1491fe-0701-4c76-8cb4-1be041dbad87"), "SecondarySpecial" },
                    { new Guid("b50842e5-77bc-47dd-8c18-b4be6c68c80b"), "Higher" },
                    { new Guid("ed10f980-6d61-487b-a2a1-7224ece8467b"), "Secondary" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("17fb7cce-96a5-45cc-9e3f-f170ce183457"), "EnterAnswer" },
                    { new Guid("354e8081-11e3-409d-acc4-c484f1456c23"), "MultipleAnswer" },
                    { new Guid("e8614741-3f5b-4b90-9a8c-aff19defeb1a"), "OneAnswer" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0770d5ad-3a2f-4d0e-9ed4-6fda5da57861"), "Manager" },
                    { new Guid("0ff5bc58-08a1-4e94-8448-174b1f047921"), "User" },
                    { new Guid("bd396515-6996-4cf1-ad47-3080d68e1e04"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DataCreated", "EducationId", "Email", "IsActivated", "IsDeleted", "Login", "Password", "RoleId" },
                values: new object[] { new Guid("77e4ea5e-26f7-4c76-aeb1-1dd447856135"), new DateTime(2022, 12, 7, 12, 24, 22, 932, DateTimeKind.Local).AddTicks(8173), new Guid("b50842e5-77bc-47dd-8c18-b4be6c68c80b"), "admin@mail.ru", true, false, "admin@", "F8450A97CC7E38E6D109425C87B41634", new Guid("bd396515-6996-4cf1-ad47-3080d68e1e04") });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "Adress", "Birthday", "FirstName", "LastName", "PhoneNumber", "SurName", "UserId" },
                values: new object[] { new Guid("b288fbb4-fac9-4404-9f50-b8d6408d5036"), "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "", "", "", new Guid("77e4ea5e-26f7-4c76-aeb1-1dd447856135") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
