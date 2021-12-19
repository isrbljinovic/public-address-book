using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PublicAddressBook.Migrations
{
    public partial class PopulateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_Address",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_Name",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactId", "Address", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"), "Tina Ujevica 3, Krizevci", new DateTime(1997, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivan Srbljinovic" },
                    { new Guid("c536dde5-4e5b-440c-9801-74d4a8fc7440"), "Trg bana Jelacica 1, Zagreb", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe" },
                    { new Guid("cd03283d-bc37-46fe-a974-915860680b5d"), "Trg bana Jelacica 1, Zagreb", new DateTime(1991, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Doe" }
                });

            migrationBuilder.InsertData(
                table: "TelephoneNumbers",
                columns: new[] { "TelephoneNumberId", "ContactId", "Number" },
                values: new object[,]
                {
                    { new Guid("1296ee0a-d753-4cb4-924b-25b32ed86506"), new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"), "+3850901234567" },
                    { new Guid("99713f98-b250-4706-93b8-4fdaeb10e082"), new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"), "+3850907654321" },
                    { new Guid("709d116e-8341-410f-8078-19562ecdfb3d"), new Guid("cd03283d-bc37-46fe-a974-915860680b5d"), "+3850912345678" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name_Address",
                table: "Contacts",
                columns: new[] { "Name", "Address" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_Name_Address",
                table: "Contacts");

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: new Guid("c536dde5-4e5b-440c-9801-74d4a8fc7440"));

            migrationBuilder.DeleteData(
                table: "TelephoneNumbers",
                keyColumn: "TelephoneNumberId",
                keyValue: new Guid("1296ee0a-d753-4cb4-924b-25b32ed86506"));

            migrationBuilder.DeleteData(
                table: "TelephoneNumbers",
                keyColumn: "TelephoneNumberId",
                keyValue: new Guid("709d116e-8341-410f-8078-19562ecdfb3d"));

            migrationBuilder.DeleteData(
                table: "TelephoneNumbers",
                keyColumn: "TelephoneNumberId",
                keyValue: new Guid("99713f98-b250-4706-93b8-4fdaeb10e082"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: new Guid("6aef342c-b2a0-410e-b8e2-d41df80fb360"));

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "ContactId",
                keyValue: new Guid("cd03283d-bc37-46fe-a974-915860680b5d"));

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Address",
                table: "Contacts",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_Name",
                table: "Contacts",
                column: "Name",
                unique: true);
        }
    }
}
