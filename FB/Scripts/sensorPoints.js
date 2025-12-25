// name工程部位的id
// id 截面的编号
// 数据里的id 传感器的编号
// 写成方法是防止变量被修改
function getPoints() {
    var sensorPoints = [
        // 2号应急
        {
            name: '2YJ+00',
            id:'2YJ+00',
            data: [
                { id: '2YJ+00YB04', x: 487, y: 84, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB03', x: 354, y: 67, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB09', x: 588, y: 69, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB05', x: 351, y: 44, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB02', x: 487, y: 52, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB06', x: 593, y: 46, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB01', x: 352, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB08', x: 463, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00YB07', x: 593, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2YJ+00WY04', x: 475, y: 84, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY03', x: 366, y: 67, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY09', x: 576, y: 69, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY05', x: 363, y: 44, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY02', x: 475, y: 52, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY06', x: 581, y: 46, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY01', x: 364, y: 16, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY08', x: 475, y: 16, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00WY07', x: 581, y: 16, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2YJ+00ZD01', x: 463, y: 84, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2YJ+00ZD02', x: 378, y: 67, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2YJ+00ZD03', x: 564, y: 69, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2YJ+00ZD04', x: 375, y: 44, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2YJ+00ZD05', x: 463, y: 52, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 569, y: 46, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 376, y: 16, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 487, y: 16, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 569, y: 16, marker: { symbol: 'cricle', radius: 5 } },
            ],
            color: 'green'
        },
        // 4号应急
        {
            name: '4YJ+00',
            id: '4YJ+00',
            data: [
                { id: '4YJ+00YB04', x: 487, y: 84, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB03', x: 354, y: 67, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB09', x: 588, y: 69, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB05', x: 351, y: 44, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB02', x: 487, y: 52, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB06', x: 593, y: 46, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB01', x: 352, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB08', x: 463, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00YB07', x: 593, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4YJ+00WY04', x: 475, y: 84, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY03', x: 366, y: 67, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY09', x: 576, y: 69, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY05', x: 363, y: 44, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY02', x: 475, y: 52, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY06', x: 581, y: 46, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY01', x: 364, y: 16, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY08', x: 475, y: 16, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00WY07', x: 581, y: 16, marker: { symbol: 'triangle', radius: 5 } },
                { id: '4YJ+00ZD01', x: 463, y: 84, marker: { symbol: 'cricle', radius: 5 } },
                { id: '4YJ+00ZD02', x: 378, y: 67, marker: { symbol: 'cricle', radius: 5 } },
                { id: '4YJ+00ZD03', x: 564, y: 69, marker: { symbol: 'cricle', radius: 5 } },
                { id: '4YJ+00ZD04', x: 375, y: 44, marker: { symbol: 'cricle', radius: 5 } },
                { id: '4YJ+00ZD05', x: 463, y: 52, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 569, y: 46, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 376, y: 16, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 487, y: 16, marker: { symbol: 'cricle', radius: 5 } },
                { id: 'p1', x: 569, y: 16, marker: { symbol: 'cricle', radius: 5 } },
            ],
            color: 'green'
        },
        // 2号竖井
        {
            name: '2SJ+00',
            id:'2SJ+00',
            data: [
                { id: '2SJ+00YL04', x: 485, y: 88, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '2SJ+00YL03', x: 328, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '2SJ+00YL05', x: 635, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '2SJ+00YL02', x: 259, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+00YL06', x: 710, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+00YL01', x: 270, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+00YL07', x: 696, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+00GJ04', x: 485, y: 85, marker: { symbol: 'square', radius: 5 } },
                { id: '2SJ+00GJ03', x: 333, y: 73, marker: { symbol: 'square', radius: 5 } },
                { id: '2SJ+00GJ05', x: 629, y: 73, marker: { symbol: 'square', radius: 5 } },
                { id: '2SJ+00GJ02', x: 277, y: 43, marker: { symbol: 'square', radius: 5 } },
                { id: '2SJ+00GJ06', x: 695, y: 43, marker: { symbol: 'square', radius: 5 } },
                { id: '2SJ+00GJ01', x: 290, y: 16, marker: { symbol: 'square', radius: 5 } },
                { id: '2SJ+00GJ07', x: 680, y: 16, marker: { symbol: 'square', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2SJ+01',
            id: '2SJ+01',
            data: [
                { id: '2SJ+01YL04', x: 485, y: 88, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '2SJ+01YL03', x: 328, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '2SJ+01YL05', x: 635, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '2SJ+01YL02', x: 259, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+01YL06', x: 710, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+01YL01', x: 270, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2SJ+01YL07', x: 696, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } }
            ],
            color: 'green'
        },
        // 4号竖井
        {
            name: '4SJ+00',
            id: '4SJ+00',
            data: [
                { id: '4SJ+00YL04', x: 485, y: 88, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '4SJ+00YL03', x: 328, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '4SJ+00YL05', x: 635, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '4SJ+00YL02', x: 259, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+00YL06', x: 710, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+00YL01', x: 270, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+00YL07', x: 696, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+00GJ04', x: 485, y: 85, marker: { symbol: 'square', radius: 5 } },
                { id: '4SJ+00GJ03', x: 333, y: 73, marker: { symbol: 'square', radius: 5 } },
                { id: '4SJ+00GJ05', x: 629, y: 73, marker: { symbol: 'square', radius: 5 } },
                { id: '4SJ+00GJ02', x: 277, y: 43, marker: { symbol: 'square', radius: 5 } },
                { id: '4SJ+00GJ06', x: 695, y: 43, marker: { symbol: 'square', radius: 5 } },
                { id: '4SJ+00GJ01', x: 290, y: 16, marker: { symbol: 'square', radius: 5 } },
                { id: '4SJ+00GJ07', x: 680, y: 16, marker: { symbol: 'square', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '4SJ+01',
            id: '4SJ+01',
            data: [
                { id: '4SJ+01YL04', x: 485, y: 88, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '4SJ+01YL03', x: 328, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '4SJ+01YL05', x: 635, y: 76, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 18, height: 8 } },
                { id: '4SJ+01YL02', x: 259, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+01YL06', x: 710, y: 43, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+01YL01', x: 270, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '4SJ+01YL07', x: 696, y: 16, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } }
            ],
            color: 'green'
        },
        // 2号正洞
        {
            name: 'DK490+410',
            id:'DK490+410',
            data: [
                { id: '90+410YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+410YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+410YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+410YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+410WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+410WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+410WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+410WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+410ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+415',
            id: 'DK490+415',
            data: [
                { id: '90+415YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+415YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+415YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+415YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+415WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+415WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+415WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+415WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+415ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+420',
            id: 'DK490+420',
            data: [
                { id: '90+420YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+420YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+420YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+420YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+420WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+420WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+420WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+420WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+420ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+425',
            id: 'DK490+425',
            data: [
                { id: '90+425YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+425YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+425YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+425YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+425WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+425WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+425WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+425WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+425ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+430',
            id: 'DK490+430',
            data: [
                { id: '90+430YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+430YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+430YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+430YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+430WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+430WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+430WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+430WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+430ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+435',
            id: 'DK490+435',
            data: [
                { id: '90+435YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+435YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+435YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+435YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+435WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+435WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+435WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+435WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+435ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+440',
            id: 'DK490+440',
            data: [
                { id: '90+440YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+440YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+440YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+440YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+440WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+440WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+440WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+440WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+440ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+445',
            id: 'DK490+445',
            data: [
                { id: '90+445YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+445YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+445YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+445YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+445WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+445WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+445WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+445WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+445ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+450',
            id: 'DK490+450',
            data: [
                { id: '90+450YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+450YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+450YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+450YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+450WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+450WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+450WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+450WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+450ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+455',
            id: 'DK490+455',
            data: [
                { id: '90+455YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+455YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+455YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+455YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+455WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+455WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+455WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+455WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+455ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+460',
            id: 'DK490+460',
            data: [
                { id: '90+460YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+460YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+460YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+460YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+460WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+460WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+460WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+460WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+460ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+480',
            id: 'DK490+480',
            data: [
                { id: '90+480YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+480YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+480YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+480YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+480WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+480WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+480WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+480WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+480ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+500',
            id: 'DK490+500',
            data: [
                { id: '90+500YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+500YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+500YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+500YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+500WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+500WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+500WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+500WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+500ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+520',
            id: 'DK490+520',
            data: [
                { id: '90+520YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+520YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+520YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+520YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+520WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+520WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+520WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+520WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+520ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+540',
            id: 'DK490+540',
            data: [
                { id: '90+540YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+540YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+540YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+540YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+540WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+540WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+540WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+540WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+540ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+560',
            id: 'DK490+560',
            data: [
                { id: '90+560YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+560YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+560YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+560YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+560WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+560WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+560WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+560WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+560ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+580',
            id: 'DK490+580',
            data: [
                { id: '90+580YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+580YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+580YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+580YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+580WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+580WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+580WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+580WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+580ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+592',
            id: 'DK490+592',
            data: [
                { id: '90+592YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+592YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+592YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+592YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+592WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+592WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+592WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+592WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+592ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+597',
            id: 'DK490+597',
            data: [
                { id: '90+597YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+597YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+597YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+597YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+597WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+597WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+597WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+597WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+597ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+602',
            id: 'DK490+602',
            data: [
                { id: '90+602YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+602YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+602YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+602YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+602WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+602WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+602WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+602WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+602ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+607',
            id: 'DK490+607',
            data: [
                { id: '90+607YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+607YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+607YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+607YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+607WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+607WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+607WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+607WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+607ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+612',
            id: 'DK490+612',
            data: [
                { id: '90+612YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+612YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+612YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+612YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+612WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+612WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+612WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+612WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+612ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+617',
            id: 'DK490+617',
            data: [
                { id: '90+617YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+617YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+617YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+617YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+617WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+617WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+617WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+617WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+617ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+625',
            id: 'DK490+625',
            data: [
                { id: '90+625YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+625YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+625YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+625YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+625WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+625WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+625WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+625WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+625ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+630',
            id: 'DK490+630',
            data: [
                { id: '90+630YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+630YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+630YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+630YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+630WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+630WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+630WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+630WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+630ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+635',
            id: 'DK490+635',
            data: [
                { id: '90+635YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+635YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+635YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+635YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+635WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+635WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+635WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+635WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+635ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+640',
            id: 'DK490+640',
            data: [
                { id: '90+640YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+640YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+640YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+640YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+640WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+640WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+640WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+640WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+640ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+645',
            id: 'DK490+645',
            data: [
                { id: '90+645YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+645YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+645YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+645YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+645WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+645WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+645WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+645WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+645ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+650',
            id: 'DK490+650',
            data: [
                { id: '90+650YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+650YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+650YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+650YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+650WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+650WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+650WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+650WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+650ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK490+670',
            id: 'DK490+670',
            data: [
                { id: '90+670YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+670YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+670YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+670YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '90+670WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+670WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+670WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+670WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '90+670ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        // 4号正洞
        {
            name: 'DK493+880',
            id: 'DK493+880',
            data: [
                { id: '93+880YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+880YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+880YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+880YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+880WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+880WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+880WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+880WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+880ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+900',
            id: 'DK493+900',
            data: [
                { id: '93+900YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+900YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+900YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+900YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+900WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+900WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+900WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+900WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+900ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+914',
            id: 'DK493+914',
            data: [
                { id: '93+914YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+914YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+914YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+914YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+914WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+914WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+914WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+914WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+914ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+919',
            id: 'DK493+919',
            data: [
                { id: '93+919YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+919YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+919YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+919YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+919WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+919WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+919WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+919WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+919ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+924',
            id: 'DK493+924',
            data: [
                { id: '93+924YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+924YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+924YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+924YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+924WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+924WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+924WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+924WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+924ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+929',
            id: 'DK493+929',
            data: [
                { id: '93+929YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+929YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+929YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+929YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+929WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+929WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+929WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+929WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+929ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+934',
            id: 'DK493+934',
            data: [
                { id: '93+934YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+934YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+934YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+934YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+934WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+934WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+934WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+934WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+934ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+939',
            id: 'DK493+939',
            data: [
                { id: '93+939YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+939YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+939YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+939YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+939WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+939WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+939WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+939WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+939ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+944',
            id: 'DK493+944',
            data: [
                { id: '93+944YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+944YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+944YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+944YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+944WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+944WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+944WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+944WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+944ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+949',
            id: 'DK493+949',
            data: [
                { id: '93+949YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+949YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+949YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+949YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+949WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+949WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+949WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+949WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+949ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+954',
            id: 'DK493+954',
            data: [
                { id: '93+954YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+954YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+954YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+954YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+954WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+954WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+954WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+954WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+954ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+959',
            id: 'DK493+959',
            data: [
                { id: '93+959YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+959YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+959YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+959YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+959WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+959WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+959WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+959WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+959ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+964',
            id: 'DK493+964',
            data: [
                { id: '93+964YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+964YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+964YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+964YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+964WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+964WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+964WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+964WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+964ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+970',
            id: 'DK493+970',
            data: [
                { id: '93+970YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+970YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+970YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+970YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+970WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+970WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+970WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+970WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+970ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+975',
            id: 'DK493+975',
            data: [
                { id: '93+975YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+975YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+975YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+975YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+975WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+975WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+975WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+975WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+975ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+980',
            id: 'DK493+980',
            data: [
                { id: '93+980YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+980YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+980YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+980YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+980WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+980WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+980WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+980WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+980ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+985',
            id: 'DK493+985',
            data: [
                { id: '93+985YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+985YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+985YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+985YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+985WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+985WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+985WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+985WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+985ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+990',
            id: 'DK493+990',
            data: [
                { id: '93+990YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+990YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+990YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+990YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+990WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+990WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+990WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+990WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+990ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK493+995',
            id: 'DK493+995',
            data: [
                { id: '93+995YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+995YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+995YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+995YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '93+995WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+995WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+995WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+995WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '93+995ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+005',
            id: 'DK494+005',
            data: [
                { id: '94+005YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+005YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+005YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+005YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+005WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+005WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+005WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+005WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+005ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+010',
            id: 'DK494+010',
            data: [
                { id: '94+010YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+010YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+010YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+010YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+010WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+010WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+010WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+010WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+010ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+015',
            id: 'DK494+015',
            data: [
                { id: '94+015YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+015YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+015YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+015YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+015WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+015WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+015WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+015WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+015ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+020',
            id: 'DK494+020',
            data: [
                { id: '94+020YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+020YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+020YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+020YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+020WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+020WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+020WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+020WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+020ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+025',
            id: 'DK494+025',
            data: [
                { id: '94+025YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+025YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+025YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+025YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+025WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+025WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+025WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+025WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+025ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+030',
            id: 'DK494+030',
            data: [
                { id: '94+030YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+030YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+030YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+030YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+030WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+030WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+030WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+030WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+030ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+040',
            id: 'DK494+040',
            data: [
                { id: '94+040YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+040YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+040YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+040YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+040WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+040WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+040WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+040WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+040ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+060',
            id: 'DK494+060',
            data: [
                { id: '94+060YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+060YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+060YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+060YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+060WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+060WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+060WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+060WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+060ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: 'DK494+080',
            id: 'DK494+080',
            data: [
                { id: '94+080YB02', x: 326, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+080YB03', x: 641, y: 73, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+080YB01', x: 302, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+080YB04', x: 674, y: 47, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '94+080WY02', x: 338, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+080WY03', x: 629, y: 73, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+080WY01', x: 314, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+080WY04', x: 662, y: 47, marker: { symbol: 'triangle', radius: 5 } },
                { id: '94+080ZS01', x: 650, y: 47, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        // 2号斜井
        {
            name: '2X0+07',
            id:'2X0+07',
            data: [
                { id: '2X0+07YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+07YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+07WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+07WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+07ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+07ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+12',
            id: '2X0+12',
            data: [
                { id: '2X0+12YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+12YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+12WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+12WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+12ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+12ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+17',
            id: '2X0+17',
            data: [
                { id: '2X0+17YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+17YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+17WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+17WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+17ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+17ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+22',
            id: '2X0+22',
            data: [
                { id: '2X0+22YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+22YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+22WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+22WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+22ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+22ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+27',
            id: '2X0+27',
            data: [
                { id: '2X0+27YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+27YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+27WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+27WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+27ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+27ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+32',
            id: '2X0+32',
            data: [
                { id: '2X0+32YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+32YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+32WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+32WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+32ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+32ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+47',
            id: '2X0+47',
            data: [
                { id: '2X0+47YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+47YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+47WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+47WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+47ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+47ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+52',
            id: '2X0+52',
            data: [
                { id: '2X0+52YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+52YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+52WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+52WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+52ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+52ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+57',
            id: '2X0+57',
            data: [
                { id: '2X0+57YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+57YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+57WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+57WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+57ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+57ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+67',
            id: '2X0+67',
            data: [
                { id: '2X0+67YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+67YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+67WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+67WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+67ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+67ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+72',
            id: '2X0+72',
            data: [
                { id: '2X0+72YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+72YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+72WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+72WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+72ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+72ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '2X0+77',
            id: '2X0+77',
            data: [
                { id: '2X0+77YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+77YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '2X0+77WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+77WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '2X0+77ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '2X0+77ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        // 4号斜井
        {
            name: '3X0+10',
            id: '3X0+10',
            data: [
                { id: '3X0+10YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+10YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+10WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+10WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+10ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X0+10ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X0+15',
            id: '3X0+15',
            data: [
                { id: '3X0+15YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+15YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+15WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+15WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+15ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X0+15ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X0+20',
            id: '3X0+20',
            data: [
                { id: '3X0+20YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+20YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+20WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+20WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+20ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X0+20ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X0+25',
            id: '3X0+25',
            data: [
                { id: '3X0+25YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+25YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+25WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+25WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+25ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X0+25ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X0+30',
            id: '3X0+30',
            data: [
                { id: '3X0+30YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+30YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+30WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+30WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+30ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X0+30ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X0+35',
            id: '3X0+35',
            data: [
                { id: '3X0+35YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+35YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X0+35WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+35WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X0+35ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X0+35ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X1+15',
            id: '3X1+15',
            data: [
                { id: '3X1+15YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+15YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+15WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+15WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+15ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X1+15ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X1+20',
            id: '3X1+20',
            data: [
                { id: '3X1+20YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+20YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+20WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+20WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+20ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X1+20ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X1+25',
            id: '3X1+25',
            data: [
                { id: '3X1+25YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+25YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+25WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+25WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+25ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X1+25ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X1+35',
            id: '3X1+35',
            data: [
                { id: '3X1+35YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+35YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+35WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+35WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+35ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X1+35ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X1+40',
            id: '3X1+40',
            data: [
                { id: '3X1+40YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+40YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+40WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+40WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+40ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X1+40ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        },
        {
            name: '3X1+45',
            id: '3X1+45',
            data: [
                { id: '3X1+45YB01', x: 299, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+45YB02', x: 683, y: 34, marker: { symbol: 'url(../picture/rectangle_g.png)', width: 8, height: 18 } },
                { id: '3X1+45WY01', x: 311, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+45WY02', x: 671, y: 34, marker: { symbol: 'triangle', radius: 5 } },
                { id: '3X1+45ZS01', x: 323, y: 34, marker: { symbol: 'cricle', radius: 5 } },
                { id: '3X1+45ZS02', x: 659, y: 34, marker: { symbol: 'cricle', radius: 5 } }
            ],
            color: 'green'
        }
    ]
    return sensorPoints;
}