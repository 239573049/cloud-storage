# token

## 服务部署
1. drone实现CI/CD 自动部署


## 服务架构
1. 采用模块化整理，DDD思想
2. ORM采用EF core框架 数据库Mysql
3. 仓储使用abp通用仓储

## 简介
部署建议使用drone部署，运行指向.drone.yml直接部署

运行项目的时候请先部署mysql和redis服务
然后更改appsettings.json配置文件