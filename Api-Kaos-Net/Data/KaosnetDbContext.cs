using System;
using System.Collections.Generic;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Api_Kaos_Net.Data;

public partial class KaosnetDbContext : DbContext
{
    public KaosnetDbContext()
    {
    }

    public KaosnetDbContext(DbContextOptions<KaosnetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConversionRate> ConversionRates { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Password> Passwords { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleAccess> RoleAccesses { get; set; }

    public virtual DbSet<SalesAccount> SalesAccounts { get; set; }

    public virtual DbSet<StreamingAccount> StreamingAccounts { get; set; }

    public virtual DbSet<StreamingType> StreamingTypes { get; set; }

    public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }

    public virtual DbSet<SubscriptionPlanAccount> SubscriptionPlanAccounts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=173.212.254.152;port=53306;database=kaosnet;user=kaosnet_admin;password=9KGaD5HRto0g5WBE", ServerVersion.Parse("11.4.7-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_spanish2_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<ConversionRate>(entity =>
        {
            entity.HasKey(e => e.ConversionRateId).HasName("PRIMARY");

            entity.ToTable("CONVERSION_RATE");

            entity.HasIndex(e => e.ConversionRateId, "conversion_rate_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ParentConversionRateFk, "fk_CONVERSION_RATE_CONVERSION_RATE1_idx");

            entity.HasIndex(e => e.CurrencyFromId, "fk_CONVERSION_RATE_CURRENCY1_idx");

            entity.HasIndex(e => e.CurrencyToId, "fk_CONVERSION_RATE_CURRENCY2_idx");

            entity.Property(e => e.ConversionRateId)
                .HasColumnType("int(11)")
                .HasColumnName("conversion_rate_id");
            entity.Property(e => e.AmountRate)
                .HasPrecision(10, 4)
                .HasColumnName("amount_rate");
            entity.Property(e => e.ConversionRateStatus)
                .HasMaxLength(45)
                .HasColumnName("conversion_rate_status");
            entity.Property(e => e.CurrencyFromId)
                .HasColumnType("int(11)")
                .HasColumnName("currency_from_id");
            entity.Property(e => e.CurrencyToId)
                .HasColumnType("int(11)")
                .HasColumnName("currency_to_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.IsReversed)
                .HasDefaultValueSql("'0'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_reversed");
            entity.Property(e => e.ParentConversionRateFk)
                .HasColumnType("int(11)")
                .HasColumnName("parent_conversion_rate_fk");
            entity.Property(e => e.ValidDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("valid_date");

            entity.HasOne(d => d.CurrencyFrom).WithMany(p => p.ConversionRateCurrencyFroms)
                .HasForeignKey(d => d.CurrencyFromId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CONVERSION_RATE_CURRENCY1");

            entity.HasOne(d => d.CurrencyTo).WithMany(p => p.ConversionRateCurrencyTos)
                .HasForeignKey(d => d.CurrencyToId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CONVERSION_RATE_CURRENCY2");

            entity.HasOne(d => d.ParentConversionRateFkNavigation).WithMany(p => p.InverseParentConversionRateFkNavigation)
                .HasForeignKey(d => d.ParentConversionRateFk)
                .HasConstraintName("fk_CONVERSION_RATE_CONVERSION_RATE1");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.CurrencyId).HasName("PRIMARY");

            entity.ToTable("CURRENCY");

            entity.HasIndex(e => e.CurrencyId, "currency_id_UNIQUE").IsUnique();

            entity.Property(e => e.CurrencyId)
                .HasColumnType("int(11)")
                .HasColumnName("currency_id");
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(45)
                .HasColumnName("currency_code");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(100)
                .HasColumnName("currency_name");
            entity.Property(e => e.CurrencyStatus)
                .HasMaxLength(45)
                .HasColumnName("currency_status");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.Symbol)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("symbol");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("CUSTOMER");

            entity.HasIndex(e => e.CustomerId, "customer_id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.SubscriptionPlanFk, "fk_CUSTOMER_SUBSCRIPTION_PLAN1_idx");

            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(45)
                .HasColumnName("contact_number");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(255)
                .HasColumnName("customer_name");
            entity.Property(e => e.CustomerStatus)
                .HasMaxLength(45)
                .HasColumnName("customer_status");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.SubscriptionPlanFk)
                .HasColumnType("int(11)")
                .HasColumnName("subscription_plan_fk");

            entity.HasOne(d => d.SubscriptionPlanFkNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.SubscriptionPlanFk)
                .HasConstraintName("fk_CUSTOMER_SUBSCRIPTION_PLAN1");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("PRIMARY");

            entity.ToTable("MODULE");

            entity.HasIndex(e => e.ModuleId, "module_id_UNIQUE").IsUnique();

            entity.Property(e => e.ModuleId)
                .HasColumnType("int(11)")
                .HasColumnName("module_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.MenuSequence)
                .HasColumnType("int(2)")
                .HasColumnName("menu_sequence");
            entity.Property(e => e.ModuleDescription)
                .HasMaxLength(255)
                .HasColumnName("module_description");
            entity.Property(e => e.ModuleIcon)
                .HasMaxLength(45)
                .HasColumnName("module_icon");
            entity.Property(e => e.ModuleName)
                .HasMaxLength(100)
                .HasColumnName("module_name");
            entity.Property(e => e.ModuleStatus)
                .HasMaxLength(45)
                .HasColumnName("module_status");
        });

        modelBuilder.Entity<Password>(entity =>
        {
            entity.HasKey(e => e.PasswordId).HasName("PRIMARY");

            entity.ToTable("PASSWORD");

            entity.HasIndex(e => e.UserFk, "fk_PASSWORD_USER1_idx");

            entity.HasIndex(e => e.PasswordId, "password_id_UNIQUE").IsUnique();

            entity.Property(e => e.PasswordId)
                .HasColumnType("int(11)")
                .HasColumnName("password_id");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("created_date");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("timestamp")
                .HasColumnName("expiration_date");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.Password1)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PasswordStatus)
                .HasMaxLength(45)
                .HasColumnName("password_status");
            entity.Property(e => e.UserFk)
                .HasColumnType("int(11)")
                .HasColumnName("user_fk");

            entity.HasOne(d => d.UserFkNavigation).WithMany(p => p.Passwords)
                .HasForeignKey(d => d.UserFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PASSWORD_USER1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("ROLE");

            entity.HasIndex(e => e.RoleId, "role_id_UNIQUE").IsUnique();

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.RoleDescription)
                .HasMaxLength(255)
                .HasColumnName("role_description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .HasColumnName("role_name");
            entity.Property(e => e.RoleStatus)
                .HasMaxLength(45)
                .HasColumnName("role_status");
        });

        modelBuilder.Entity<RoleAccess>(entity =>
        {
            entity.HasKey(e => e.RoleAccessId).HasName("PRIMARY");

            entity.ToTable("ROLE_ACCESS");

            entity.HasIndex(e => e.RoleFk, "fk_ROLE_ACCESS_ROLE_idx");

            entity.HasIndex(e => e.ViewFk, "fk_ROLE_ACCESS_VIEW1_idx");

            entity.HasIndex(e => e.RoleAccessId, "role_access_id_UNIQUE").IsUnique();

            entity.HasIndex(e => new { e.RoleFk, e.ViewFk }, "uk_ROLE_ACCESS_role_view");

            entity.Property(e => e.RoleAccessId)
                .HasColumnType("int(11)")
                .HasColumnName("role_access_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.IsWrite)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_write");
            entity.Property(e => e.RoleAccessStatus)
                .HasMaxLength(45)
                .HasColumnName("role_access_status");
            entity.Property(e => e.RoleFk)
                .HasColumnType("int(11)")
                .HasColumnName("role_fk");
            entity.Property(e => e.ViewFk)
                .HasColumnType("int(11)")
                .HasColumnName("view_fk");

            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.RoleAccesses)
                .HasForeignKey(d => d.RoleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ROLE_ACCESS_ROLE");

            entity.HasOne(d => d.ViewFkNavigation).WithMany(p => p.RoleAccesses)
                .HasForeignKey(d => d.ViewFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ROLE_ACCESS_VIEW1");
        });

        modelBuilder.Entity<SalesAccount>(entity =>
        {
            entity.HasKey(e => e.IdSalesAccount).HasName("PRIMARY");

            entity.ToTable("SALES_ACCOUNT");

            entity.HasIndex(e => e.CurrencyFk, "fk_SALES_ACCOUNT_CURRENCY1_idx");

            entity.HasIndex(e => e.CustomerId, "fk_SALES_ACCOUNT_CUSTOMER1_idx");

            entity.HasIndex(e => e.StreamingAccountFk, "fk_SALES_ACCOUNT_STREAMING_ACCOUNT1_idx");

            entity.HasIndex(e => e.IdSalesAccount, "id_sales_account_UNIQUE").IsUnique();

            entity.Property(e => e.IdSalesAccount)
                .HasColumnType("int(11)")
                .HasColumnName("id_sales_account");
            entity.Property(e => e.AmountSales)
                .HasPrecision(10, 2)
                .HasColumnName("amount_sales");
            entity.Property(e => e.ContactEmail)
                .HasMaxLength(255)
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(45)
                .HasColumnName("contact_number");
            entity.Property(e => e.CurrencyFk)
                .HasColumnType("int(11)")
                .HasColumnName("currency_fk");
            entity.Property(e => e.CustomerId)
                .HasColumnType("int(11)")
                .HasColumnName("customer_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.ProfileNumber)
                .HasColumnType("int(2)")
                .HasColumnName("profile_number");
            entity.Property(e => e.ProfilePin)
                .HasMaxLength(45)
                .HasColumnName("profile_pin");
            entity.Property(e => e.SalesAccountStatus)
                .HasMaxLength(45)
                .HasColumnName("sales_account_status");
            entity.Property(e => e.SalesDate)
                .HasColumnType("timestamp")
                .HasColumnName("sales_date");
            entity.Property(e => e.StreamingAccountFk)
                .HasColumnType("int(11)")
                .HasColumnName("streaming_account_fk");

            entity.HasOne(d => d.CurrencyFkNavigation).WithMany(p => p.SalesAccounts)
                .HasForeignKey(d => d.CurrencyFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SALES_ACCOUNT_CURRENCY1");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesAccounts)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_SALES_ACCOUNT_CUSTOMER1");

            entity.HasOne(d => d.StreamingAccountFkNavigation).WithMany(p => p.SalesAccounts)
                .HasForeignKey(d => d.StreamingAccountFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SALES_ACCOUNT_STREAMING_ACCOUNT1");
        });

        modelBuilder.Entity<StreamingAccount>(entity =>
        {
            entity.HasKey(e => e.StreamingAccountId).HasName("PRIMARY");

            entity.ToTable("STREAMING_ACCOUNT");

            entity.HasIndex(e => e.StreamingTypeFk, "fk_STREAMING_ACCOUNT_STREAMING_TYPE1_idx");

            entity.HasIndex(e => e.StreamingAccountId, "streaming_account_id_UNIQUE").IsUnique();

            entity.Property(e => e.StreamingAccountId)
                .HasColumnType("int(11)")
                .HasColumnName("streaming_account_id");
            entity.Property(e => e.AccountEmail)
                .HasMaxLength(255)
                .HasColumnName("account_email");
            entity.Property(e => e.AccountPassword)
                .HasMaxLength(255)
                .HasColumnName("account_password");
            entity.Property(e => e.ExpiredDate)
                .HasColumnType("timestamp")
                .HasColumnName("expired_date");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.MaximumQuantityProfiles)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(2)")
                .HasColumnName("maximum_quantity_profiles");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProfilesQuantity)
                .HasColumnType("int(2)")
                .HasColumnName("profiles_quantity");
            entity.Property(e => e.StreamingAccountName)
                .HasMaxLength(100)
                .HasColumnName("streaming_account_name");
            entity.Property(e => e.StreamingAccountStatus)
                .HasMaxLength(45)
                .HasColumnName("streaming_account_status");
            entity.Property(e => e.StreamingTypeFk)
                .HasColumnType("int(11)")
                .HasColumnName("streaming_type_fk");
            entity.Property(e => e.ValidDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("valid_date");

            entity.HasOne(d => d.StreamingTypeFkNavigation).WithMany(p => p.StreamingAccounts)
                .HasForeignKey(d => d.StreamingTypeFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_STREAMING_ACCOUNT_STREAMING_TYPE1");
        });

        modelBuilder.Entity<StreamingType>(entity =>
        {
            entity.HasKey(e => e.StreamingTypeId).HasName("PRIMARY");

            entity.ToTable("STREAMING_TYPE");

            entity.HasIndex(e => e.StreamingTypeId, "streaming_type_id_UNIQUE").IsUnique();

            entity.Property(e => e.StreamingTypeId)
                .HasColumnType("int(11)")
                .HasColumnName("streaming_type_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.StreamingTypeDescription)
                .HasMaxLength(255)
                .HasColumnName("streaming_type_description");
            entity.Property(e => e.StreamingTypeName)
                .HasMaxLength(100)
                .HasColumnName("streaming_type_name");
            entity.Property(e => e.StreamingTypeStatus)
                .HasMaxLength(45)
                .HasColumnName("streaming_type_status");
        });

        modelBuilder.Entity<SubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.SubscriptionPlanId).HasName("PRIMARY");

            entity.ToTable("SUBSCRIPTION_PLAN");

            entity.HasIndex(e => e.SubscriptionPlanId, "subscription_plan_id_UNIQUE").IsUnique();

            entity.Property(e => e.SubscriptionPlanId)
                .HasColumnType("int(11)")
                .HasColumnName("subscription_plan_id");
            entity.Property(e => e.AmountTotal)
                .HasPrecision(10, 2)
                .HasColumnName("amount_total");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.SubscriptionPlanDescription)
                .HasMaxLength(255)
                .HasColumnName("subscription_plan_description");
            entity.Property(e => e.SubscriptionPlanName)
                .HasMaxLength(100)
                .HasColumnName("subscription_plan_name");
            entity.Property(e => e.SubscriptionPlanStatus)
                .HasMaxLength(45)
                .HasColumnName("subscription_plan_status");
        });

        modelBuilder.Entity<SubscriptionPlanAccount>(entity =>
        {
            entity.HasKey(e => e.SubscriptionPlanAccountId).HasName("PRIMARY");

            entity.ToTable("SUBSCRIPTION_PLAN_ACCOUNT");

            entity.HasIndex(e => e.StreamingAccountFk, "fk_SUBSCRIPTION_PLAN_ACCOUNT_STREAMING_ACCOUNT1_idx");

            entity.HasIndex(e => e.SubscriptionPlanFk, "fk_SUBSCRIPTION_PLAN_ACCOUNT_SUBSCRIPTION_PLAN1_idx");

            entity.HasIndex(e => e.SubscriptionPlanAccountId, "subscription_plan_account_id_UNIQUE").IsUnique();

            entity.HasIndex(e => new { e.StreamingAccountFk, e.SubscriptionPlanFk }, "uk_SUBSCRIPTION_PLAN_ACCOUNT_subscription_plan_streaming_account");

            entity.Property(e => e.SubscriptionPlanAccountId)
                .HasColumnType("int(11)")
                .HasColumnName("subscription_plan_account_id");
            entity.Property(e => e.AmountSubTotal)
                .HasPrecision(10, 2)
                .HasColumnName("amount_sub_total");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.QuantityAccounts)
                .HasColumnType("int(11)")
                .HasColumnName("quantity_accounts");
            entity.Property(e => e.StreamingAccountFk)
                .HasColumnType("int(11)")
                .HasColumnName("streaming_account_fk");
            entity.Property(e => e.SubscriptionPlanAccountStatus)
                .HasMaxLength(45)
                .HasColumnName("subscription_plan_account_status");
            entity.Property(e => e.SubscriptionPlanFk)
                .HasColumnType("int(11)")
                .HasColumnName("subscription_plan_fk");

            entity.HasOne(d => d.StreamingAccountFkNavigation).WithMany(p => p.SubscriptionPlanAccounts)
                .HasForeignKey(d => d.StreamingAccountFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SUBSCRIPTION_PLAN_ACCOUNT_STREAMING_ACCOUNT1");

            entity.HasOne(d => d.SubscriptionPlanFkNavigation).WithMany(p => p.SubscriptionPlanAccounts)
                .HasForeignKey(d => d.SubscriptionPlanFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_SUBSCRIPTION_PLAN_ACCOUNT_SUBSCRIPTION_PLAN1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("USER");

            entity.HasIndex(e => e.RoleFk, "fk_USER_ROLE1_idx");

            entity.HasIndex(e => e.UserId, "user_id_UNIQUE").IsUnique();

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.CurrentSessions)
                .HasColumnType("int(2)")
                .HasColumnName("current_sessions");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FailedLogin)
                .HasColumnType("int(2)")
                .HasColumnName("failed_login");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.RoleFk)
                .HasColumnType("int(11)")
                .HasColumnName("role_fk");
            entity.Property(e => e.SecretAnswer)
                .HasMaxLength(255)
                .HasColumnName("secret_answer");
            entity.Property(e => e.SecurityQuestion)
                .HasMaxLength(255)
                .HasColumnName("security_question");
            entity.Property(e => e.SessionTime)
                .HasColumnType("int(10)")
                .HasColumnName("session_time");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
            entity.Property(e => e.UserStatus)
                .HasMaxLength(45)
                .HasColumnName("user_status");

            entity.HasOne(d => d.RoleFkNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_USER_ROLE1");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.ViewId).HasName("PRIMARY");

            entity.ToTable("VIEW");

            entity.HasIndex(e => e.ModuleFk, "fk_VIEW_MODULE1_idx");

            entity.HasIndex(e => e.ParentViewFk, "fk_VIEW_VIEW1_idx");

            entity.HasIndex(e => e.ViewId, "view_id_UNIQUE").IsUnique();

            entity.Property(e => e.ViewId)
                .HasColumnType("int(11)")
                .HasColumnName("view_id");
            entity.Property(e => e.Idsession)
                .HasColumnType("int(11)")
                .HasColumnName("idsession");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("is_active");
            entity.Property(e => e.ModuleFk)
                .HasColumnType("int(11)")
                .HasColumnName("module_fk");
            entity.Property(e => e.ModuleSequence)
                .HasColumnType("int(2)")
                .HasColumnName("module_sequence");
            entity.Property(e => e.ParentViewFk)
                .HasColumnType("int(11)")
                .HasColumnName("parent_view_fk");
            entity.Property(e => e.ViewDescription)
                .HasMaxLength(255)
                .HasColumnName("view_description");
            entity.Property(e => e.ViewIcon)
                .HasMaxLength(45)
                .HasColumnName("view_icon");
            entity.Property(e => e.ViewName)
                .HasMaxLength(100)
                .HasColumnName("view_name");
            entity.Property(e => e.ViewPath)
                .HasMaxLength(45)
                .HasColumnName("view_path");
            entity.Property(e => e.ViewStatus)
                .HasMaxLength(45)
                .HasColumnName("view_status");

            entity.HasOne(d => d.ModuleFkNavigation).WithMany(p => p.Views)
                .HasForeignKey(d => d.ModuleFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VIEW_MODULE1");

            entity.HasOne(d => d.ParentViewFkNavigation).WithMany(p => p.InverseParentViewFkNavigation)
                .HasForeignKey(d => d.ParentViewFk)
                .HasConstraintName("fk_VIEW_VIEW1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
