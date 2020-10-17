
interface MenuItemBase {
    title: string;
    route: string;
    icon: string;
}

type MenuItemChild = MenuItemBase;

export interface MenuItem extends MenuItemBase {
    description?: string;
    children?: MenuItemChild[];
}
