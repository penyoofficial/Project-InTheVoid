# 「虚空之中」用户文档 Ver. 0.2

欢迎体验“虚空之中”！这是一款基于 Unity2D 的类银河恶魔城游戏，支持键鼠和控制器游玩。

## 控制

> 控制器以 Xbox Console 为例。

| 按键                                                                                          | 功能                                                                                    |
| --------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------- |
| <kbd>A</kbd> 或 ![左摇杆](./Assets/30px-XboxOne_Left_Stick.png) 左                            | 向左移动                                                                                |
| <kbd>D</kbd> 或 ![左摇杆](./Assets/30px-XboxOne_Left_Stick.png) 右                            | 向右移动                                                                                |
| 左 <kbd>Shift</kbd> 或 ![左摇杆](./Assets/30px-XboxOne_Left_Stick.png) 下按                   | 猛冲                                                                                    |
| <kbd>Space</kbd> 或 ![A](./Assets/30px-XboxOne_A.png)                                         | 跳跃                                                                                    |
| ![鼠标左键](./Assets/40px-Keyboard_Black_Mouse_Left.png) 或 ![X](./Assets/30px-XboxOne_X.png) | 普通攻击。无论用户按击多快，都会受到攻速系统的强行阻滞。当附近有 NPC 时，改为与其对话。 |
| <kbd>Q</kbd> 或 ![Y](./Assets/30px-XboxOne_Y.png)                                             | 长按蓄力重击，并造成明显的击退。在最终造成伤害前，松开都会导致释放失败。                |
| <kbd>E</kbd> 或 ![B](./Assets/30px-XboxOne_B.png)                                             | 使用技能                                                                                |
| <kbd>Tab</kbd> 或 ![左肩键](./Assets/30px-XboxOne_LB.png)                                     | 切换技能                                                                                |

## 技能

你可以部署它们之一到技能位上。

| 名称 | 描述                                                                                                                                                         | 获取方式               |
| ---- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ | ---------------------- |
| 射击 | 向光标或右摇杆指向方向掷出一枚射弹，遇到墙壁或敌方单位则以自身为中心，令 100 半径圆周内全部敌方单位受到 80 伤害。消耗 5 魔力。                               | 向梅尔支付 30 代币购买 |
| 抓索 | 向光标或右摇杆指向方向抛出一根带钩爪的绳索，遇到墙壁则将自己拖拽到附着点附近，随即松开钩爪。消耗 10 魔力。                                                   | 向梅尔支付 80 代币购买 |
| 魅惑 | 以自身为中心，令 750 半径圆周内全部敌方单位无害化（相当于自己隐身且不会受到来自敌方单位的伤害，但仍会受到环境的影响），持续 5s。效果不能叠加。消耗 50 魔力。 | 在特定的木匣里发现     |
| 悲歌 | 消耗自身最大生命值 50% 的生命（不足则扣至剩余 1 点），换取 70% 的免伤和 30% 的攻速加成，持续 25s。效果不能叠加。消耗 50 魔力。                               | 击败腐败的向阳花       |
