local LuaBehaviour = class("LuaBehaviour");

-- 脚本的命名规范：
-- 小驼峰为主
-- 文件名:大驼峰
-- 类名:大驼峰
function LuaBehaviour:onAwake()
end

function LuaBehaviour:onStart()
end

function LuaBehaviour:onEnable()
end

function LuaBehaviour:onDisable()
end

function LuaBehaviour:onDestroy()
end

function LuaBehaviour:onUpdate(dt)
end

function LuaBehaviour:setArg(key, value)
    self[key] = value;
end

return LuaBehaviour;