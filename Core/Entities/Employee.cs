namespace Core.Entities;

public class Employee
{
    /// <summary>
    /// 账户ID（唯一标识，支持UUID）
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// 公司ID
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// 员工ID
    /// </summary>
    public string EmpId { get; set; } = "";

    /// <summary>
    /// 员工姓名
    /// </summary>
    public string EmpName { get; set; } = "";

    /// <summary>
    /// 员工手机号
    /// </summary>
    public string EmpMobilePhone { get; set; } = "";

    /// <summary>
    /// 员工邮箱
    /// </summary>
    public string EmpEmail { get; set; } = "";

    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? EmpEntryDate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime? EmpDepartureDate { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public string DeptId { get; set; } = "";

    /// <summary>
    /// 成本中心ID
    /// </summary>
    public string CostCenterId { get; set; } = "";

    /// <summary>
    /// 职位类别ID
    /// </summary>
    public string JobCategoryId { get; set; } = "";

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 直接上级标识
    /// </summary>
    public string Direct { get; set; } = "";

    /// <summary>
    /// 交付日期
    /// </summary>
    public DateTime? DeliveredDate { get; set; }

    /// <summary>
    /// 生日
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string Sex { get; set; } = "";

    /// <summary>
    /// 工作年限
    /// </summary>
    public int WorkYear { get; set; }

    /// <summary>
    /// 教育程度
    /// </summary>
    public string EducationName { get; set; } = "";

    /// <summary>
    /// 创建人AccountId
    /// </summary>
    public string CreatedBy { get; set; } = "";

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 修改人AccountId
    /// </summary>
    public string ModifiedBy { get; set; } = "";

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime ModifiedTime { get; set; }
}