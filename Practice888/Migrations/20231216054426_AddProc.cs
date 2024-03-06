using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practice888.Migrations
{
    public partial class AddProc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         var sp1 = @"create proc IC 
         @CID int,  
         @CatName varchar(50)
        as begin insert into Categories(CatName) values(@CatName)      
         end";
            migrationBuilder.Sql(sp1);

            var sp2 = @"create proc GC 
        
        as begin select*from  Categories     
         end";
            migrationBuilder.Sql(sp2);

            var sp3 = @"create proc UC 
         @CID int,  
         @CatName varchar(50)
        as begin  update Categories set CatName=@CatName where @CID=CID     
         end";
            migrationBuilder.Sql(sp3);

            var sp4 = @"create proc DC 
         @CID int 
        as begin  delete Categories  where @CID=CID     
         end";
            migrationBuilder.Sql(sp4);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
