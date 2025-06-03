## CorePlatformAPI

### 框架设计

### 一、整体分层架构设计

```
CorePlatformAPI
├── CorePlatformAPI.API/          # 表现层（WebAPI入口）
├── CorePlatformAPI.Application/  # 应用服务层（业务逻辑协调）
├── CorePlatformAPI.Core/         # 核心层（领域模型与接口）
└── CorePlatformAPI.Infrastructure/ # 基础设施层（具体实现）
```

### 二、各层功能与实现细节

#### 1. CorePlatformAPI.API（接口层）

* 职责：处理HTTP请求、响应、路由、认证、Swagger文档等。

    * 文件夹结构示例：

```
API/
├── Controllers/           # API控制器
├── Middlewares/           # 自定义中间件
├── Filters/               # 过滤器
├── appsettings.json       # 配置文件
└── Program.cs             # 入口文件
```

#### 2. CorePlatformAPI.Application（应用服务层）

* 职责：协调领域逻辑、工作流、事务管理，不直接操作数据库。

    * 文件夹结构示例：

```
Application/
├── Services/              # 应用服务
├── Commands/              # CQRS模式中的命令
├── Queries/               # CQRS模式中的查询
├── Interfaces/            # 服务接口
└── Mappings/              # AutoMapper配置
```

#### 3. CorePlatformAPI.Core（核心层）

* 职责：定义领域模型、仓储接口、通用工具类，不依赖其他层。

    * 文件夹结构示例：

```
Core/
├── Contracts/          # 接口自定义类
│   ├── Requests/       # 接口请求类
│   └── Results/        # 接口返回类
├── DTOs/               # 实体自定义格式化转出类
├── Entities/           # 实体类
├── Enums/              # 枚举
├── Exceptions/         # 扩展方法
├── Helpers/            # 工具类
└── Interfaces/         # 仓储服务
```

#### 4. CorePlatformAPI.Infrastructure（基础设施层）

* 职责：实现核心层定义的接口，处理数据库、缓存、日志等具体技术。

    * 文件夹结构示例：

```
Infrastructure/
├── Data/                  # 数据库上下文与Dapper实现
│   ├── Repositories/      # 仓储实现
│   ├── DbConnectionFactory.cs # 数据库连接工厂
│   └── DapperExtensions.cs    # Dapper扩展方法
├── Logging/               # 日志实现
├── Caching/               # 缓存实现
├── Services/              # 基础服务实现（邮箱，通知，短信，钉钉）
└── DependencyInjection/   # 依赖注入配置
```

### 系统功能

- 多租户（切换数据库）
- 翻译
- 接口认证（自定义token，踢人下线，登录日志，延长token，开放接口实现，单点登录）
- 用户管理
- 机构管理
- 职位管理
- 菜单管理
- 角色管理
- 字典管理
- 通知管理（SignalR）
- HttpLogging

## 快速开始

1. 事务案例
    ```csharp
   IUnitOfWork unitOfWork
       
    try
        {
            await unitOfWork.BeginTransactionAsync();

            await unitOfWork.CommitAsync();
            return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
        }
        catch (Exception exception)
        {
            await unitOfWork.RollbackAsync();
            throw new BadRequestException(MsgCodeEnum.Error, exception.Message);
        }
    ```   































