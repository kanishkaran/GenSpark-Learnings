using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SocialMediaAPI.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "hashtags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hashtags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tweets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PostedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tweets_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userFollows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FollowerId = table.Column<int>(type: "integer", nullable: false),
                    FollowingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userFollows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userFollows_users_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userFollows_users_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tweetComments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    CommentedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweetComments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_tweetComments_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tweetComments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tweetHashtags",
                columns: table => new
                {
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    HashtagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweetHashtags", x => x.TweetId);
                    table.ForeignKey(
                        name: "FK_tweetHashtags_hashtags_HashtagId",
                        column: x => x.HashtagId,
                        principalTable: "hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tweetHashtags_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tweetLikes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TweetId = table.Column<int>(type: "integer", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tweetLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tweetLikes_tweets_TweetId",
                        column: x => x.TweetId,
                        principalTable: "tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tweetLikes_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tweetComments_TweetId",
                table: "tweetComments",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetComments_UserId",
                table: "tweetComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetHashtags_HashtagId",
                table: "tweetHashtags",
                column: "HashtagId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetLikes_TweetId",
                table: "tweetLikes",
                column: "TweetId");

            migrationBuilder.CreateIndex(
                name: "IX_tweetLikes_UserId",
                table: "tweetLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tweets_UserId",
                table: "tweets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_userFollows_FollowerId",
                table: "userFollows",
                column: "FollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_userFollows_FollowingId",
                table: "userFollows",
                column: "FollowingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tweetComments");

            migrationBuilder.DropTable(
                name: "tweetHashtags");

            migrationBuilder.DropTable(
                name: "tweetLikes");

            migrationBuilder.DropTable(
                name: "userFollows");

            migrationBuilder.DropTable(
                name: "hashtags");

            migrationBuilder.DropTable(
                name: "tweets");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
