module.exports = {
  locales: {
    // 键名是该语言所属的子路径
    // 作为特例，默认语言可以使用 '/' 作为其路径。
    '/': {
      lang: 'en-US', // 将会被设置为 <html> 的 lang 属性
      title: 'PasteEx',
      description: 'Paste As File'
    },
    '/zh-CN/': {
      lang: 'zh-CN',
      title: 'PasteEx',
      description: '粘贴为文件'
    }
  },
  title: '网站标题',
  description: '网站描述',
  // 注入到当前页面的 HTML <head> 中的标签
  head: [
    ['link', { rel: 'icon', href: '/favicon.ico' }], // 增加一个自定义的 favicon(网页标签的图标)
  ],
  themeConfig: {
  	sidebarDepth: 2,
    // the GitHub repo path
    repo: "huiyadanli/PasteEx",
    // the label linking to the repo
    repoLabel: "GitHub",
    locales: {
      '/': {
      	// text for the language dropdown
        selectText: 'Languages',
        // label for this locale in the language dropdown
        label: 'English',
        // Custom text for edit link. Defaults to "Edit this page"
        editLinkText: 'Edit this page on GitHub',
        // Custom navbar values
        nav: [{ text: "Guide", link: "/guide/" }],
        // Custom sidebar values
        sidebar: [
          "/",
          ["/guide/", "Guide"],
        ]
      },
      '/zh-CN/': {
        // 多语言下拉菜单的标题
        selectText: '选择语言',
        // 该语言在下拉菜单中的标签
        label: '简体中文',
        // 编辑链接文字
        editLinkText: '在 GitHub 上编辑此页',
        nav: [{ text: "指南", link: "/zh-CN/guide/" }],
        sidebar: [
          ["/zh-CN/", "首页"],
          ["/zh-CN/guide/", "指南"],
        ]
      }
    }
  }
};